using System.CommandLine;

namespace ReaperMan.Cli.Extension;

public class UninstallCommand : Command
{
    public UninstallCommand() : base("uninstall", "Uninstall an extension from a Reaper installation")
    {
    }
}