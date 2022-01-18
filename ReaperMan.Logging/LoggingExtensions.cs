using Microsoft.Extensions.Configuration;
using Serilog;

namespace ReaperMan.Logging;

public static class LoggingExtensions
{
    public static void AddLogger(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console()
            .CreateLogger();
    }
}