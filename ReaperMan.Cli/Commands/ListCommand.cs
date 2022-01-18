using System.CommandLine;

namespace ReaperMan.Cli;

public class ListCommand : Command
{
    public ListCommand() : base("list", "List Reaper installations")
    {
    }
}