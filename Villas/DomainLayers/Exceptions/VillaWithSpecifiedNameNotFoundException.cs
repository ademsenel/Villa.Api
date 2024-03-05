using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class VillaWithSpecifiedNameNotFoundException : ServiceNotFoundException
{
    const string ReasonString = $"{nameof(Villa)} {nameof(Villa.Name)} Not Found Exception";

    public override string Reason => ReasonString;

    public VillaWithSpecifiedNameNotFoundException()
    {
    }

    public VillaWithSpecifiedNameNotFoundException(string message) : base(message)
    {
    }

    public VillaWithSpecifiedNameNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}