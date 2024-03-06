using Villas.DomainLayers.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.Validators;

internal static class VillaValidator
{
    internal static void EnsureIdIsValid(string propertyName, int villaId)
    {
        var errorMessage = Validator.ValidateIntegerProperty(propertyName, villaId);
        EnSureNoErrors(errorMessage);
    }

    internal static void EnsureNameIsValid(string propertyName, string villaName)
    {
        var errorMessage = Validator.ValidateStringProperty(propertyName, villaName);
        EnSureNoErrors(errorMessage);
    }

    internal static void EnsureVillaIsValid(Villa villa)
    {
        EnsureVillaIsNotNull(villa);
        var cleanedUpVilla = EnsureNullablePropertiesCleanUp(villa);
        var errorMessage = ValidateProperties(cleanedUpVilla);
        EnSureNoErrors(errorMessage);
    }

    private static string ValidateProperties(Villa villa)
    {
        var sb = new StringBuilder();
        sb.Append(Validator.ValidateStringProperty(nameof(Villa.Name), villa.Name));
        sb.Append(Validator.ValidateDoubleProperty(nameof(Villa.Rate), villa.Rate));
        sb.Append(Validator.ValidateIntegerProperty(nameof(Villa.Sqft), villa.Sqft));
        sb.Append(Validator.ValidateIntegerProperty(nameof(Villa.Occupancy), villa.Occupancy));
        sb.Append(Validator.ValidateStringProperty(nameof(Villa.ImageUrl), villa.ImageUrl));
        var errorMessage = sb.ToString();
        return errorMessage.Length == 0 ? null : errorMessage;
    }

    private static void EnsureVillaIsNotNull(Villa villa)
    {
        if (villa == null)
            throw new VillaNullException($"The {nameof(Villa).ToLower(CultureInfo.InvariantCulture)} parameter can not be null.");
    }

    private static void EnSureNoErrors(string errorMessage)
    {
        if (errorMessage != null)
            throw new VillaValidationException(errorMessage);
    }

    private static Villa EnsureNullablePropertiesCleanUp(Villa villa)
    {
        string villaDetails = EnsureNullablePropertyCleanUp(villa);
        string villaAmenity = EnsureNullablePropertyCleanUp(villa);
        return new Villa(villa.Id, villa.Name, villaDetails, villa.Rate, villa.Sqft, villa.Occupancy, villa.ImageUrl, villaAmenity);
    }

    private static string EnsureNullablePropertyCleanUp(Villa villa)
    {
        string villaDetails = string.Empty;
        if (!villa.Details.IsNullOrEmpty())
            villaDetails = villa.Details;
        return villaDetails;
    }
}
