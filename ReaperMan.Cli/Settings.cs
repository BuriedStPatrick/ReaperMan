namespace ReaperMan.Cli;

public interface IInstallationSettings
{
    string? DownloadsPath { get; set; }
}

public class Settings : IInstallationSettings
{
    public string? DownloadsPath { get; set; }
}
