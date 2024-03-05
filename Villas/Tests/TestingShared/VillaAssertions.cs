using System.Globalization;
using System.Text;
using Villas.DomainLayers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Shared.EqualityComparers;

namespace Testing.Shared;

public static class VillaAssertions
{
    public static void AssertVillasAreEqual(IEnumerable<Villa> expectedVillas, IEnumerable<Villa> actualVillas)
    {
        var villaEqualityComparer = new VillaEqualityComparer();
        var villasNotInActual = expectedVillas.Except(actualVillas, villaEqualityComparer);
        var villasNotInExpected = actualVillas.Except(expectedVillas, villaEqualityComparer);

        if (!villasNotInActual.Any() && !villasNotInExpected.Any())
            return;

        var errorMessage = new StringBuilder();
        if (villasNotInActual.Any())
        {
            errorMessage.AppendLine(CultureInfo.InvariantCulture, $"The Following {nameof(Villa)}s are in Expected {nameof(Villa)}s but not in Actual {nameof(Villa)}s.");
            foreach (var villa in villasNotInActual)
                errorMessage.AppendLine(CultureInfo.InvariantCulture, $"{nameof(villa.Name)}: {villa.Name}, {nameof(villa.Details)}: {villa.Details}, {nameof(villa.Rate)}: {villa.Rate}, {nameof(villa.Sqft)}: {villa.Sqft}, {nameof(villa.Occupancy)}: {villa.Occupancy}, {nameof(villa.ImageUrl)}: {villa.ImageUrl}, {nameof(villa.Amenity)}: {villa.Amenity}");
        }
        if (villasNotInExpected.Any())
        {
            errorMessage.AppendLine(CultureInfo.InvariantCulture, $"The Following {nameof(Villa)}s are in Actual {nameof(Villa)}s but not in Expected {nameof(Villa)}s.");
            foreach (var villa in villasNotInExpected)
                errorMessage.AppendLine(CultureInfo.InvariantCulture, $"{nameof(villa.Name)}: {villa.Name}, {nameof(villa.Details)}: {villa.Details}, {nameof(villa.Rate)}: {villa.Rate}, {nameof(villa.Sqft)}: {villa.Sqft}, {nameof(villa.Occupancy)}: {villa.Occupancy}, {nameof(villa.ImageUrl)}: {villa.ImageUrl}, {nameof(villa.Amenity)}: {villa.Amenity}");
        }

        throw new AssertFailedException(errorMessage.ToString());
    }
}
