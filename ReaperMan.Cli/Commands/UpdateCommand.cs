using System.CommandLine;
using ReaperMan.Core.Services;

namespace ReaperMan.Cli;

public class UpdateCommand : Command
{
    public UpdateCommand(IInstallationService installationService) : base("update", "Update a Reaper installation")
    {
    }
}