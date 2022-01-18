using System.Diagnostics;
using System.Text.RegularExpressions;
using Flurl.Http;
using ReaperMan.Cli;
using Serilog;

namespace ReaperMan.Core.Services;

public interface IInstallationService
{
    Task Install(
        InstallOptions? installOptions,
        CancellationToken cancellationToken);

    Task<string?> GetLatestVersion(
        Architecture architecture,
        CancellationToken cancellationToken);
}

public record InstallOptions(
    string Edition,
    Architecture Architecture,
    bool? Silent,
    bool? Portable,
    string? Destination);

public enum Architecture
{
    x86,
    x64
}

public class InstallationService : IInstallationService
{
    private readonly IInstallationSettings _installationSettings;

    public InstallationService(IInstallationSettings installationSettings)
    {
        _installationSettings = installationSettings;
    }

    public async Task<string?> GetLatestVersion(
        Architecture architecture,
        CancellationToken cancellationToken)
    {
        var html = await "https://www.reaper.fm/download.php"
            .GetStringAsync(cancellationToken);

        var regex = architecture switch
        {
            Architecture.x64 => new Regex(@"files\/.*\/reaper(.*)_x64-install.exe"),
            Architecture.x86 => new Regex(@"files\/.*\/reaper(.*)_x86-install.exe"),
            _ => throw new ArgumentOutOfRangeException(nameof(architecture))
        };

        var match = regex.Match(html);
        return match.Success
            ? match.Groups[1].Value
            : null;
    }

    private async Task<string> Download(
        Architecture architecture,
        string edition,
        string destination,
        CancellationToken cancellationToken)
    {
        return await $"https://www.reaper.fm/files/{edition[0]}.x/reaper{edition}_{architecture}-install.exe"
            .DownloadFileAsync(
                destination,
                cancellationToken: cancellationToken);
    }

    public async Task Install(
        InstallOptions? installOptions,
        CancellationToken cancellationToken)
    {
        var downloadDestination = _installationSettings.DownloadsPath ?? Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");

        Log.Information("Downloading REAPER. Edition: {Edition}, Architecture: {Architecture}", 
            installOptions.Edition,
            installOptions.Architecture);

        // Download installer
        var filePath = await Download(
            installOptions.Architecture,
            installOptions.Edition,
            downloadDestination,
            cancellationToken);

        Log.Information("Downloaded REAPER. Edition: {Edition}, Architecture: {Architecture}", 
            installOptions.Edition,
            installOptions.Architecture);

        Log.Debug("Downloaded to {Destination}", filePath);

        // Run installer
        var process = new Process();
        process.StartInfo.FileName = filePath;

        // Construct arguments
        var arguments = new List<string>();

        if (installOptions.Silent == true)
        {
            arguments.Add("/S");
        }

        if (installOptions.Portable == true)
        {
            arguments.Add("/PORTABLE");
        }

        if (installOptions.Destination != null)
        {
            arguments.Add($"/D={installOptions.Destination}");
        }

        process.StartInfo.Arguments = string.Join(" ", arguments);

        if (!process.Start())
        {
            throw new Exception("Failed to run installer");
        }

        Log.Information("Installing REAPER. Edition: {Edition}, Architecture: {Architecture}", 
            installOptions.Edition,
            installOptions.Architecture);

        await process.WaitForExitAsync(cancellationToken);
    }
}