using System.CommandLine;

namespace ReaperMan.Cli;

public class RemoveCommand : Command
{
    public RemoveCommand() : base("remove", "Stop managing a specific Reaper installation with ReaperMan (will *not* uninstall it)")
    {
    }
}