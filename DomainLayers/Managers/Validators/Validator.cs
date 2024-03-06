using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.Validators;

internal enum StringState
{
    Null,
    Empty,
    WhiteSpace,
    Valid
}

internal static class Validator
{
    public static string ValidateIntegerProperty(string propertyName, int propertyValue)
    {
        if (propertyValue <= 0)
            return $"The {nameof(Villa)} {propertyName} must be a valid {propertyName} and can not be less than {propertyValue}.\r\n";
        return null;
    }

    public static string ValidateStringProperty(string propertyName, string propertyValue)
    {
        return DetermineNullEmptyOrWhiteSpaces(propertyValue) switch
        {
            StringState.Null => $"The {nameof(Villa)} {propertyName} must be a valid {propertyName} and can not be null.\r\n",
            StringState.Empty => $"The {nameof(Villa)} {propertyName} must be a valid {propertyName} and can not be empty.\r\n",
            StringState.WhiteSpace => $"The {nameof(Villa)} {propertyName} must be a valid {propertyName} and can not be whitespaces.\r\n",
            _ => null,
        };
    }

    public static string ValidateDoubleProperty(string propertyName, double propertyValue)
    {
        if (propertyValue <= 0)
            return $"The {nameof(Villa)} {propertyName} must be a valid {propertyName} and can not be less than {propertyValue}.\r\n";
        return null;
    }

    private static StringState DetermineNullEmptyOrWhiteSpaces(string data)
    {
        if (data == null)
            return StringState.Null;
        else if (data.Length == 0)
            return StringState.Empty;
        foreach (var item in data)
            if (!char.IsWhiteSpace(item))
                return StringState.Valid;

        return StringState.WhiteSpace;
    }

}
