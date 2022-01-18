using System.CommandLine;
using ReaperMan.Core.Services;

namespace ReaperMan.Cli;

public class UninstallCommand : Command
{
    public UninstallCommand(IInstallationService installationService) : base("uninstall", "Uninstall a Reaper installation")
    {
    }
}