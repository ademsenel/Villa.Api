using Villas.Api.Middleware.Translator;

namespace Villas.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            await ExceptionToHttpTranslator.Translate(httpContext, e);
        }
    }
}

public static class AppBuilderExtensions
{
    public static void UseCustomExceptionHandling(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionHandlingMiddleware>();

}
