using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class VillaValidationException : BusinessBaseException
{
    const string ReasonString = "Validation Error";

    public override string Reason => ReasonString;

    public VillaValidationException()
    {
    }

    public VillaValidationException(string message) : base(message)
    {
    }

    public VillaValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}