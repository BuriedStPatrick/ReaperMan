using System.CommandLine;

namespace ReaperMan.Cli.Extension;

public class InstallCommand : Command
{
    public InstallCommand() : base("install", "Install an extension like reapack or SWS extensions from a Reaper installation")
    {
    }
}