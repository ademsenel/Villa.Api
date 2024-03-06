using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class BaseException : Exception
{
    [ExcludeFromCodeCoverage]
    public abstract string Reason { get; }

    protected BaseException()
    {
    }

    protected BaseException(string message) : base(message)
    {
    }

    protected BaseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
