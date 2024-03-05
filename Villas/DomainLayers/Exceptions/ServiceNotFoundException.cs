using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class ServiceNotFoundException : BusinessBaseException
{
    protected ServiceNotFoundException()
    {
    }

    protected ServiceNotFoundException(string message) : base(message)
    {
    }

    protected ServiceNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}