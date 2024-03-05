using Microsoft.VisualStudio.TestTools.UnitTesting;
using Villas.DomainLayers.Models;
using Testing.Shared.TestingHelpers;
using Testing.Shared;
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace Villas.ClassTests;

[TestClass]
public sealed class RasAssertionsTests
{
    [TestMethod]
    [TestCategory("Class Test")]
    public void AssertVillasAreEqual_WhenExpectedAndActualAreEqualWithRandomGenerator_NotExceptionsIsThrown()
    {
        // Arrange
        var villa1 = RandomGenerator.GenerateRandomVillas(1).Single();
        var villa2 = RandomGenerator.GenerateRandomVillas(1).Single();

        var expectedVillas = new List<Villa> { villa1, villa2 };

        var actualVillas = new List<Villa> { villa1, villa2 };

        // Act

        // Assert
        VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void AssertVillasAreEqual_WhenExpectedAndActualAreEqual_NotExceptionsIsThrown()
    {
        // Arrange
        var expectedVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (2, "Beach Villa", "Lorem Ipsum Beach Villa", 20.50, 200, 20, "http://www.ras.technology/2.jpg", "Near Beach")
        };

        var actualVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (2, "Beach Villa", "Lorem Ipsum Beach Villa", 20.50, 200, 20, "http://www.ras.technology/2.jpg", "Near Beach")
        };

        // Act
        // Assert
        VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void AssertVillasAreEqual_WhenExpectedHasVillasNotInActual_Throws()
    {
        // Arrange
        var expectedVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (3, "Beach Villa Double", "Lorem Ipsum Beach Villa Double", 30.50, 300, 30, "http://www.ras.technology/3.jpg", "Near Beach Double"),
            new (4, "Pool Villa Double", "Lorem Ipsum Pool Villa Double", 40.50, 400, 40, "http://www.ras.technology/4.jpg", "Near Road Double"),
            new (5, "Beach Villa Triplex", "Lorem Ipsum Beach Villa Triplex", 50.50, 500, 50, "http://www.ras.technology/5.jpg", "Near Beach")
        };

        var actualVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (2, "Beach Villa", "Lorem Ipsum Beach Villa", 20.50, 200, 20, "http://www.ras.technology/2.jpg", "Near Beach")
        };

        try
        {
            // Act
            VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
        }
        catch (AssertFailedException e)
        {
            // Assert
            AssertEx.EnsureExceptionMessageContains(
                e,
                $"The Following {nameof(Villa)}s are in Expected {nameof(Villa)}s but not in Actual {nameof(Villa)}s.",
                $"{nameof(Villa.Name)}: Beach Villa Double, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa Double, {nameof(Villa.Rate)}: 30.5, {nameof(Villa.Sqft)}: 300, {nameof(Villa.Occupancy)}: 30, {nameof(Villa.ImageUrl)}: http://www.ras.technology/3.jpg, {nameof(Villa.Amenity)}: Near Beach Double",
                $"{nameof(Villa.Name)}: Pool Villa Double, {nameof(Villa.Details)}: Lorem Ipsum Pool Villa Double, {nameof(Villa.Rate)}: 40.5, {nameof(Villa.Sqft)}: 400, {nameof(Villa.Occupancy)}: 40, {nameof(Villa.ImageUrl)}: http://www.ras.technology/4.jpg, {nameof(Villa.Amenity)}: Near Road Double",
                $"{nameof(Villa.Name)}: Beach Villa Triplex, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa Triplex, {nameof(Villa.Rate)}: 50.5, {nameof(Villa.Sqft)}: 500, {nameof(Villa.Occupancy)}: 50, {nameof(Villa.ImageUrl)}: http://www.ras.technology/5.jpg, {nameof(Villa.Amenity)}: Near Beach",
                $"The Following {nameof(Villa)}s are in Actual {nameof(Villa)}s but not in Expected {nameof(Villa)}s.",
                $"{nameof(Villa.Name)}: Beach Villa, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa, {nameof(Villa.Rate)}: 20.5, {nameof(Villa.Sqft)}: 200, {nameof(Villa.Occupancy)}: 20, {nameof(Villa.ImageUrl)}: http://www.ras.technology/2.jpg, {nameof(Villa.Amenity)}: Near Beach");
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public void AssertVillasAreEqual_WhenActualHasVillasNotInExpected_Throws()
    {
        // Arrange
        var expectedVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (3, "Beach Villa Double", "Lorem Ipsum Beach Villa Double", 30.50, 300, 30, "http://www.ras.technology/3.jpg", "Near Beach Double")
        };

        var actualVillas = new List<Villa>
        {
            new (1, "Pool Villa", "Lorem Ipsum Pool Villa", 10.50, 100, 10, "http://www.ras.technology/1.jpg", "Near Road"),
            new (2, "Beach Villa", "Lorem Ipsum Beach Villa", 20.50, 200, 20, "http://www.ras.technology/2.jpg", "Near Beach"),
            new (4, "Pool Villa Double", "Lorem Ipsum Pool Villa Double", 40.50, 400, 40, "http://www.ras.technology/4.jpg", "Near Road Double"),
            new (5, "Beach Villa Triplex", "Lorem Ipsum Beach Villa Triplex", 50.50, 500, 50, "http://www.ras.technology/5.jpg", "Near Beach")
        };

        try
        {
            // Act
            VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
        }
        catch (AssertFailedException e)
        {
            // Assert
            var expectedExceptionMessage = $"The Following {nameof(Villa)}s are in Expected {nameof(Villa)}s but not in Actual {nameof(Villa)}s.\r\n" +
                $"{nameof(Villa.Name)}: Beach Villa Double, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa Double, {nameof(Villa.Rate)}: 30.5, {nameof(Villa.Sqft)}: 300, {nameof(Villa.Occupancy)}: 30, {nameof(Villa.ImageUrl)}: http://www.ras.technology/3.jpg, {nameof(Villa.Amenity)}: Near Beach Double\r\n" +
                $"The Following {nameof(Villa)}s are in Actual {nameof(Villa)}s but not in Expected {nameof(Villa)}s.\r\n" +
                $"{nameof(Villa.Name)}: Beach Villa, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa, {nameof(Villa.Rate)}: 20.5, {nameof(Villa.Sqft)}: 200, {nameof(Villa.Occupancy)}: 20, {nameof(Villa.ImageUrl)}: http://www.ras.technology/2.jpg, {nameof(Villa.Amenity)}: Near Beach\r\n" +
                $"{nameof(Villa.Name)}: Pool Villa Double, {nameof(Villa.Details)}: Lorem Ipsum Pool Villa Double, {nameof(Villa.Rate)}: 40.5, {nameof(Villa.Sqft)}: 400, {nameof(Villa.Occupancy)}: 40, {nameof(Villa.ImageUrl)}: http://www.ras.technology/4.jpg, {nameof(Villa.Amenity)}: Near Road Double\r\n" +
                $"{nameof(Villa.Name)}: Beach Villa Triplex, {nameof(Villa.Details)}: Lorem Ipsum Beach Villa Triplex, {nameof(Villa.Rate)}: 50.5, {nameof(Villa.Sqft)}: 500, {nameof(Villa.Occupancy)}: 50, {nameof(Villa.ImageUrl)}: http://www.ras.technology/5.jpg, {nameof(Villa.Amenity)}: Near Beach\r\n";

            Assert.AreEqual(expectedExceptionMessage, e.Message);
        }
    }
}