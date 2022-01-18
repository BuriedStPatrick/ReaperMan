using System.CommandLine;
using System.CommandLine.Builder;
using Microsoft.Extensions.Configuration;
using ReaperMan.Logging;
using ReaperMan.Core;
using ReaperMan.Core.Services;
using Serilog;

namespace ReaperMan.Cli;

public class Program
{
    public static async Task<int> Main(params string[] args)
    {
        // Load settings from environment, args, etc.
        var configuration = new ConfigurationBuilder()
            .ReadConfiguration(args)
            .Build();

        var settings = configuration.Bind<Settings>();

        // Initialize logger
        LoggingExtensions.AddLogger(configuration);

        Log.Debug("{@Settings}", settings);
        
        // services
        var installationService = new InstallationService(settings);
        
        var rootCommand = new CommandBuilder("ReaperMan - Manage your Reaper installations")
            .WithRootCommand(new InstallCommand(installationService))
            .WithRootCommand(new UninstallCommand(installationService))
            .WithRootCommand(new UpdateCommand(installationService))
            .WithRootCommand(new ListCommand())
            .WithRootCommand(new ScanCommand())
            .WithRootCommand(new RemoveCommand())
            .WithRootCommand(new AddCommand())
            .WithRootCommand(new StatusCommand())
            .WithCommandScope("extension", "Manage Reaper extensions and plugins")
                .WithSubCommand(new Extension.InstallCommand())
                .WithSubCommand(new Extension.UninstallCommand())
            .Build();

        // Parse the incoming args and invoke the handler
        return await rootCommand.InvokeAsync(args);
    }
}
