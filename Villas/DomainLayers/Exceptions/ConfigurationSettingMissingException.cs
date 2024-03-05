using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class ConfigurationSettingMissingException : TechnicalBaseException
{
    private const string ReasonString = "Configuration Setting missing";

    public override string Reason => ReasonString;

    public ConfigurationSettingMissingException()
    {
    }

    public ConfigurationSettingMissingException(string message) : base(message)
    {
    }

    public ConfigurationSettingMissingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}