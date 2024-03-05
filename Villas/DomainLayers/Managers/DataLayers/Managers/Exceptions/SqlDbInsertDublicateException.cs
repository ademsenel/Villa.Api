using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Exceptions;

namespace Villas.DomainLayers.Managers.DataLayers.Managers.Exceptions;

[ExcludeFromCodeCoverage]
internal sealed class SqlDbInsertDublicateException : TechnicalBaseException
{
    const string ReasonString = "Insert dublicate data exception";

    public override string Reason => ReasonString;

    internal SqlDbInsertDublicateException()
    {
    }

    internal SqlDbInsertDublicateException(string message) : base(message)
    {
    }

    internal SqlDbInsertDublicateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}