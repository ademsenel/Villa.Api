using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class TechnicalBaseException : BaseException
{
    protected TechnicalBaseException()
    {
    }

    protected TechnicalBaseException(string message) : base(message)
    {
    }

    protected TechnicalBaseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
