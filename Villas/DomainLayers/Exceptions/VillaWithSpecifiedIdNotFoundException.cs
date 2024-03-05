using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class VillaWithSpecifiedIdNotFoundException : ServiceNotFoundException
{
    const string ReasonString = $"{nameof(Villa)} {nameof(Villa.Id)} Not Found Exception";

    public override string Reason => ReasonString;

    public VillaWithSpecifiedIdNotFoundException()
    {
    }

    public VillaWithSpecifiedIdNotFoundException(string message) : base(message)
    {
    }

    public VillaWithSpecifiedIdNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}