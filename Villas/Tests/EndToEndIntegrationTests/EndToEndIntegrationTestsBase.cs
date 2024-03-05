using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Net;
using System.Text;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Managers.ConfigurationProviders;
using Villas.DomainLayers.Models;

namespace EndToEndIntegrationTests;

public abstract class EndToEndIntegrationTestsBase
{
    protected private static HttpClient _httpClient = default!;
    protected private readonly string _url;

    protected EndToEndIntegrationTestsBase()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        var webApplicationFactory = new WebApplicationFactory<Program>();
        _httpClient = webApplicationFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(new ConfigurationProvider($"{nameof(Villa)}ApiAppSettings:").GetSettingValue("ServiceUrl"));
    }

    [ClassCleanup]
    public static void ClassCleanup() => _httpClient?.Dispose();

    protected static async Task EnsureSuccess(HttpResponseMessage httpResponseMessage)
    {
        if (httpResponseMessage.IsSuccessStatusCode)
            return;
        var httpContent = await httpResponseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        throw new AssertFailedException($"Reason Phrase: {httpResponseMessage.ReasonPhrase} with Content: {httpContent}");
    }

    protected static async Task EnsureErrorResponseIsCorrect(HttpResponseMessage httpResponseMessage, HttpStatusCode expectedStatusCode, BusinessBaseException expectedException)
    {
        var errorMessages = new StringBuilder();

        if (expectedStatusCode != httpResponseMessage.StatusCode)
            errorMessages.AppendLine(CultureInfo.InvariantCulture, $"The Expected HttpStatusCode was: {HttpStatusCode.BadRequest}, but the Actual HttpStatusCode is: {httpResponseMessage.StatusCode}.");

        if (expectedException.Reason != httpResponseMessage.ReasonPhrase)
            errorMessages.AppendLine(CultureInfo.InvariantCulture, $"The Expected Reason Phrase was: {expectedException.Reason}, but the Actual Reason Phrase is: {httpResponseMessage.ReasonPhrase}.");

        var expectedExceptionTypeHeaderValue = expectedException.GetType().Name;
        var actualExceptionTypeHeaderValue = httpResponseMessage.Headers.GetValues("Exception-Type").First();

        if (actualExceptionTypeHeaderValue == null)
            errorMessages.AppendLine("We were expecting an HTTP Header called Exception-Type, but this header was not found");
        else if (expectedExceptionTypeHeaderValue != actualExceptionTypeHeaderValue)
            errorMessages.Append(CultureInfo.InvariantCulture, $"The Expected \"Exception-Type\" Header Value: {expectedExceptionTypeHeaderValue}, but the Actual \"Exception-Type\" Header Value is:{actualExceptionTypeHeaderValue}.");

        var actualHttpContentString = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!actualHttpContentString.Contains(expectedException.Message))
            errorMessages.AppendLine(CultureInfo.InvariantCulture, $"The Expected HttpContent: {expectedException.Message}, but the Actual HttpContent is: {actualHttpContentString}.");

        if (errorMessages.Length > 0)
            throw new AssertFailedException(errorMessages.ToString());
    }
}
