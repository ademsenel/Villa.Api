using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Testing.Shared.TestingHelpers;

[ExcludeFromCodeCoverage]
public sealed class RandomStringGenerator
{
    private RandomStringGenerator() { }

    public static string GetRandomACIIString(int length)
    {
        // (97, 122) lowercase
        // (65, 90)) uppercase
        // (48, 57) numeric

        const int MinNumberForUpperCase = 65;
        const int MaxNumberForUpperCase = 90;
        const int MinNumberForLowerCase = 97;
        const int MaxNumberForLowerCase = 122;

        var sb = new StringBuilder();

        // ensure at least one of each type occurs
        sb.Append(Convert.ToChar(RandomNumberGenerator.GetInt32(MinNumberForUpperCase, MaxNumberForUpperCase))); // uppercase

        for (var i = 0; i < length - 1; i++)
            sb.Append(Convert.ToChar(RandomNumberGenerator.GetInt32(MinNumberForLowerCase, MaxNumberForLowerCase)));

        return sb.ToString();
    }

    public static int GetRandomInteger(int minimumNumber, int maximumNumber) =>
        RandomNumberGenerator.GetInt32(minimumNumber, maximumNumber);

    public static string GetRandomUrl()
    {
        const int MinNumberForDomainName = 5;
        const int MaxNumberForDomainName = 20;
        const int MinNumberForAfterName = 3;
        const int MaxNumberForAfterName = 15;
        const int MinNumberForLast = 10;

        var sb = new StringBuilder();

        sb.Append("https://www.");
        sb.Append(GetRandomACIIString(RandomNumberGenerator.GetInt32(MinNumberForDomainName, MaxNumberForDomainName)).ToLower(CultureInfo.InvariantCulture));
        sb.Append(".com/");
        sb.Append(GetRandomACIIString(RandomNumberGenerator.GetInt32(MinNumberForAfterName, MaxNumberForAfterName)).ToLower(CultureInfo.InvariantCulture));
        sb.Append('/');
        sb.Append(GetRandomACIIString(RandomNumberGenerator.GetInt32(MinNumberForLast)).ToLower(CultureInfo.InvariantCulture));
        sb.Append(".png");

        return sb.ToString();
    } 

    public static DateTime GetRandomDateTime() => 
        DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
         
    public static double GetRandomDouble() =>
        Double.Parse($"{GetRandomInteger(1, 200)}.{GetRandomInteger(0, 99)}", CultureInfo.InvariantCulture);
}