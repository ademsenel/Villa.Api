using Villas.DomainLayers.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Managers.ConfigurationProviders;

public abstract class ConfigurationProviderBase
{
    private const int ValueAsRetrievedLength = 0;

    public string GetSettingValue(string key) => RetrieveConfigurationSettingValueThrowIfMissing(key);

    [ExcludeFromCodeCoverage]
    private string RetrieveConfigurationSettingValueThrowIfMissing(string key)
    {
        var valueAsRetrieved = RetrieveConfigurationSettingValue(key);

        if (valueAsRetrieved == null)
            throw new ConfigurationSettingMissingException($"The Configuration setting with Key: {key}, is missing from the configuration file.");
        else if (valueAsRetrieved.Length == ValueAsRetrievedLength)
            throw new ConfigurationSettingValueEmptyException($"The Configuration setting with Key: {key}, exists but, value is empty");
        else if (IsWhiteSpaces(valueAsRetrieved))
            throw new ConfigurationSettingValueEmptyException($"The Configuration setting with Key: {key}, exists but its value is white space.");

        return valueAsRetrieved;
    }

    [ExcludeFromCodeCoverage]
    private static bool IsWhiteSpaces(string valueAsRetrieved)
    {
        foreach (var chr in valueAsRetrieved)
            if (!char.IsWhiteSpace(chr))
                return false;
        return true;
    }

    protected abstract string RetrieveConfigurationSettingValue(string key);
}
