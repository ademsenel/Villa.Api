using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Testing.Shared.TestingHelpers;

[ExcludeFromCodeCoverage]
public sealed class AssertEx
{
    private AssertEx() { }

    public static void EnsureExceptionMessageContains(Exception exception, params string[] expectedMessageParts)
    {
        var exceptionMessage = new StringBuilder();
        _ = exceptionMessage.Append(CultureInfo.InvariantCulture, $"An Exception of type {exception.GetType()} was thrown, however the following message parts were not found in the Exception Message.");
        _ = exceptionMessage.AppendLine(CultureInfo.InvariantCulture, $"The Actual Exception Message is {exception.Message}");

        var somePartNotFound = false;

        foreach (var part in expectedMessageParts)
            if (!exception.Message.Contains(part))
            {
                somePartNotFound = true;
                _ = exceptionMessage.AppendLine(CultureInfo.InvariantCulture, $"The Expected substring: {part}, was not found in the Exception Message.");
            }

        if (somePartNotFound)
            throw new AssertFailedException(exceptionMessage.ToString());
    }
}