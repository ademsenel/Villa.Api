using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Managers.ConfigurationProviders;

internal sealed class ConfigurationProvider : ConfigurationProviderBase
{
    private const string FileExtension = "json";
    private const string JsonFileName = "appsettings";
    private const string JsonFileNameWithExtension = JsonFileName + "." + FileExtension;
    private const string AspNetCoreEnvironmentName = "ASPNETCORE_ENVIRONMENT";
    private readonly string Key = string.Empty;

    private readonly IConfigurationRoot _configurationRoot;

    [ExcludeFromCodeCoverage]
    internal ConfigurationProvider(IConfigurationRoot configurationRoot) => _configurationRoot = configurationRoot;

    public ConfigurationProvider(string appsettingsKey, IConfigurationRoot configurationRoot = default!)
    {
        Key = appsettingsKey;
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile(JsonFileNameWithExtension);
        LoadEnvironmentSpecificAppSettings(configurationBuilder);
        if (configurationRoot == null)
            _configurationRoot = configurationBuilder.Build();
        else
            _configurationRoot = configurationRoot;
    }

    private static void LoadEnvironmentSpecificAppSettings(ConfigurationBuilder configurationBuilder)
    {
        var aspNetCoreEnvironment = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentName);
        var environmentBasedSettingsFile = JsonFileName + "." + aspNetCoreEnvironment + "." + FileExtension;

        if (File.Exists(environmentBasedSettingsFile))
            configurationBuilder.AddJsonFile(environmentBasedSettingsFile);
    }

    protected override string RetrieveConfigurationSettingValue(string key) =>
        _configurationRoot[Key + key];
}
