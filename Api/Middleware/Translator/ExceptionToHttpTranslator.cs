using Villas.DomainLayers.Exceptions;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics.CodeAnalysis;

namespace Villas.Api.Middleware.Translator;

internal static class ExceptionToHttpTranslator
{
    internal static async Task Translate([NotNull] HttpContext httpContext, [NotNull] Exception exception)
    {
        var httpResponse = httpContext.Response;
        httpResponse.Headers["Exception-Type"] = exception.GetType().Name;

        if (exception is BaseException rasException)
            httpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = rasException.Reason;

        httpResponse.StatusCode = MapExceptionToStatusCode(exception);
        await httpResponse.WriteAsync(exception.Message);
        await httpResponse.Body.FlushAsync();
    }

    private static int MapExceptionToStatusCode([NotNull] Exception exception)
    {
        if (exception is ServiceNotFoundException)
            return 404;
        else if (exception is BusinessBaseException)
            return 400;
        return 500;
    }
}