using System.CommandLine;

namespace ReaperMan.Cli;

public class ScanCommand : Command
{
    public ScanCommand() : base("scan", "Scan for Reaper installations to manage with ReaperMan")
    {
    }
}