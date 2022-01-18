using System.CommandLine;
using ReaperMan.Core.Services;
using Serilog;

namespace ReaperMan.Cli;

public class InstallCommand : Command
{
    public InstallCommand(IInstallationService installationService) : base(
        "install",
        "Add a Reaper installation")
    {
        var archOption = new Option<Architecture>(
            "--arch",
            () => Architecture.x64,
            "Architecture to install");

        var editionOption = new Option<string>(
            "--edition",
            "Edition to install (defaults to latest)");

        var silentOption = new Option<bool>(
            "--silent",
            () => false,
            "Run silent install");

        var portableOption = new Option<bool>(
            "--portable",
            () => false,
            "Install as portable");

        var destinationOption = new Option<string?>(
            "--destination",
            "Destination path for the installation");
        
        AddOption(archOption);
        AddOption(editionOption);
        AddOption(silentOption);
        AddOption(portableOption);
        AddOption(destinationOption);

        this.SetHandler(async (
            Architecture arch,
            string? edition,
            bool silent,
            bool portable,
            string? destination,
            CancellationToken cancellationToken) =>
        {
            edition ??= await installationService.GetLatestVersion(arch, cancellationToken);

            if (edition == null)
            {
                throw new ArgumentNullException(nameof(edition));
            }

            await installationService.Install(
                new InstallOptions(
                    edition,
                    arch,
                    silent,
                    portable,
                    destination),
                cancellationToken);

            Log.Information("Finished REAPER installation");
        }, archOption, editionOption, silentOption, portableOption, destinationOption);
    }
}