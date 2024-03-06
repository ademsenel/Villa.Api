using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class ConfigurationSettingValueEmptyException : TechnicalBaseException
{
    private const string ReasonString = "Configuration Setting Value Empty ";

    public override string Reason => ReasonString;

    public ConfigurationSettingValueEmptyException()
    {
    }

    public ConfigurationSettingValueEmptyException(string message) : base(message)
    {
    }

    public ConfigurationSettingValueEmptyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}