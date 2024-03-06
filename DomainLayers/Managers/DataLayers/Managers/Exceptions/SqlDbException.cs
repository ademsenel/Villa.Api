using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Exceptions;

namespace Villas.DomainLayers.Managers.DataLayers.Managers;

[ExcludeFromCodeCoverage]
internal sealed class SqlDbException : TechnicalBaseException
{
    const string ReasonString = "SqlDb exception";

    public override string Reason => ReasonString;

    internal SqlDbException()
    {
    }

    internal SqlDbException(string message) : base(message)
    {
    }

    internal SqlDbException(string message, Exception innerException) : base(message, innerException)
    {
    }
}