using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class BusinessBaseException : BaseException
{
    protected BusinessBaseException()
    {
    }

    protected BusinessBaseException(string message) : base(message)
    {
    }

    protected BusinessBaseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
