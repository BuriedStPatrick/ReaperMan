using System.CommandLine;

namespace ReaperMan.Cli;

public class AddCommand : Command
{
    public AddCommand() : base("add", "Manually start managing a Reaper installation")
    {
    }
}