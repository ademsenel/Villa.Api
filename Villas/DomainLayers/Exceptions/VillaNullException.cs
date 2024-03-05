using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class VillaNullException : ServiceNotFoundException
{
    const string ReasonString = $"{nameof(Villa)} is null";

    public override string Reason => ReasonString;

    public VillaNullException()
    {
    }

    public VillaNullException(string message) : base(message)
    {
    }

    public VillaNullException(string message, Exception innerException) : base(message, innerException)
    {
    }
}