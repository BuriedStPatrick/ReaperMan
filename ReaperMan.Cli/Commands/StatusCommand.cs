using System.CommandLine;

namespace ReaperMan.Cli;

public class StatusCommand : Command
{
    public StatusCommand() : base("status", "Print status of Reaper installation(s)")
    {
    }
}