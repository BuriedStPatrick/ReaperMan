using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;

namespace ReaperMan.Core;

public static class ConfigExtensions
{
    public static IConfigurationBuilder ReadConfiguration(this IConfigurationBuilder configurationBuilder, string[]? args)
    {
        return configurationBuilder
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .AddYamlFile("reaper.yaml");
    }

    public static TSettings Bind<TSettings>(this IConfiguration instance) where TSettings : new()
    {
        var result = new TSettings();
        instance.Bind(result);

        return result;
    }
}
