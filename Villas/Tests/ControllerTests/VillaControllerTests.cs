using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using Villas.Api.Models;
using Villas.DomainLayers.Models;
using Testing.Shared;
using ControllerTests.Controllers;
using Villas.DomainLayers.Exceptions;
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace ControllerTests;

[TestClass]
public class VillaControllerTests
{
    [TestMethod]
    [TestCategory("Class Test")]
    public async Task GetVillasAsync_WhenThereAreNoExceptions_ReturnsAllVillas()
    {
        // Arrange
        var expectedVillas = RandomGenerator.GenerateRandomVillas(50);
        var villasController = new VillaControllerForTest(MapToVillaResource(expectedVillas));

        // Act
        var actualVillaResources = await villasController.GetVillas().ConfigureAwait(false);

        // Assert
        var actualVillas = MapToVillas(actualVillaResources);
        VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task GetVillasAsync_WhenAnExceptionIsThrownInTheDomainLayer_ShouldThrow()
    {
        // Arrange
        var expectedMessage = "Some Exception Message";
        var expectedException = new ConfigurationSettingMissingException(expectedMessage);
        var villasController = new VillaControllerForTest(expectedException);

        // Act
        try
        {
            await villasController.GetVillas().ConfigureAwait(false);
            Assert.Fail($"We were expecting an exception of type: {nameof(ConfigurationSettingMissingException)} to be thrown, but no exception was thrown");
        }
        catch (ConfigurationSettingMissingException e)
        {
            // Assert
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task GetVillaByIdAsync_WhenThereAreNoExceptions_ReturnsAllVillas()
    {
        // Arrange
        var expectedVilla = RandomGenerator.GenerateRandomVillas(50)[0];
        var villasController = new VillaControllerForTest(MapVillaResource(expectedVilla));
        var validId = expectedVilla.Id;

        // Act
        var actualVillaResource = await villasController.GetVillaById(validId).ConfigureAwait(false);

        // Assert
        var actualVilla = MapToVilla(actualVillaResource);
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task GetVillaByNameAsync_WhenThereAreNoExceptions_ReturnsAllVillas()
    {
        // Arrange
        var expectedVilla = RandomGenerator.GenerateRandomVillas(50)[0];
        var villasController = new VillaControllerForTest(MapVillaResource(expectedVilla));
        var validVillaName = expectedVilla.Name;

        // Act
        var actualVillaResource = await villasController.GetVillaByName(validVillaName).ConfigureAwait(false);

        // Assert
        var actualVilla = MapToVilla(actualVillaResource);
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task CreateVillaAsync_WhenProvidedWithAValidVillaResource_Succeeds()
    {
        // Arrange
        var villaResource = new VillaResource(
            Id: 0,
            Name: "Villa",
            Details: "Villa Details",
            Rate: 0.60,
            Occupancy: 10,
            Sqft: 150,
            ImageUrl: "Image Url",
            Amenity: "Villa Amenity"
            );

        var villasController = new VillaControllerForTest(villas: null!);

        // Act
        await villasController.CreateVilla(villaResource).ConfigureAwait(false);

        // Assert
        // Nothing to Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task CreateVillaAsync_WhenAnExceptionIsThrownInTheDomainLayer_ShouldThrow()
    {
        // Arrange
        var expectedMessage = "Some Exception Message";
        var expectedException = new VillaValidationException(expectedMessage);
        var villasController = new VillaControllerForTest(expectedException);
        var villaResource = new VillaResource(
            Id: 0,
            Name: "Villa",
            Details: "Villa Details",
            Rate: 0.60,
            Occupancy: 10,
            Sqft: 150,
            ImageUrl: "Image Url",
            Amenity: "Villa Amenity"
            );

        // Act
        try
        {
            await villasController.CreateVilla(villaResource).ConfigureAwait(false);
            Assert.Fail($"We were expecting an exception of type: {nameof(VillaValidationException)} to be thrown, but no exception was thrown");
        }
        catch (VillaValidationException e)
        {
            // Assert
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task UpdateVillaAsync_WhenProvidedWithAValidVillaResource_Succeeds()
    {
        // Arrange
        var villaId = 100;
        var villaResource = new VillaResource(
            Id: villaId,
            Name: "Villa",
            Details: "Villa Details",
            Rate: 0.60,
            Occupancy: 10,
            Sqft: 150,
            ImageUrl: "Image Url",
            Amenity: "Villa Amenity"
            );

        var villasController = new VillaControllerForTest(villas: null!);

        // Act
        await villasController.UpdateVilla(villaId, villaResource).ConfigureAwait(false);

        // Assert
        // Nothing to Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task UpdateVillaAsync_WhenAnExceptionIsThrownInTheDomainLayer_ShouldThrow()
    {
        // Arrange
        var expectedMessage = "Some Exception Message";
        var expectedException = new VillaValidationException(expectedMessage);
        var villasController = new VillaControllerForTest(expectedException);
        var villaId = 120;
        var villaResource = new VillaResource(
            Id: villaId,
            Name: "Villa",
            Details: "Villa Details",
            Rate: 0.60,
            Occupancy: 10,
            Sqft: 150,
            ImageUrl: "Image Url",
            Amenity: "Villa Amenity"
            );

        // Act
        try
        {
            await villasController.UpdateVilla(villaId, villaResource).ConfigureAwait(false);
            Assert.Fail($"We were expecting an exception of type: {nameof(VillaValidationException)} to be thrown, but no exception was thrown");
        }
        catch (VillaValidationException e)
        {
            // Assert
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task DeleteVillaByIdAsync_WhenThereAreNoExceptions_DeleteVilla()
    {
        // Arrange
        var expectedVilla = RandomGenerator.GenerateRandomVillas(50)[0];
        var villasController = new VillaControllerForTest(MapVillaResource(expectedVilla));
        var validId = expectedVilla.Id;

        // Act
        await villasController.DeleteVilla(validId).ConfigureAwait(false);

        // Assert
        // Assert
        // Nothing to Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Class Test")]
    public async Task DeleteVillaByIdAsync_WhenAnExceptionIsThrownInTheDomainLayer_ShouldThrow()
    {
        // Arrange
        var expectedMessage = "Some Exception Message";
        var expectedException = new VillaValidationException(expectedMessage);
        var villasController = new VillaControllerForTest(expectedException);
        var validId = 120;

        try
        {
            // Act
            await villasController.DeleteVilla(validId).ConfigureAwait(false);
            Assert.Fail($"We were expecting an exception of type: {nameof(VillaValidationException)} to be thrown, but no exception was thrown");
        }
        catch (VillaValidationException e)
        {
            // Assert
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }

    private static Villa MapToVilla(VillaResource villaResource) =>
    new(
        Id: villaResource.Id,
        Name: villaResource.Name,
        Details: villaResource.Details,
        Rate: villaResource.Rate,
        Occupancy: villaResource.Occupancy,
        Sqft: villaResource.Sqft,
        ImageUrl: villaResource.ImageUrl,
        Amenity: villaResource.Amenity
        );

    private static ImmutableList<VillaResource> MapToVillaResource(IEnumerable<Villa> villas)
    {
        var villaResources = ImmutableList.Create<VillaResource>();
        foreach (var villa in villas)
            villaResources = villaResources.Add(MapVillaResource(villa));
        return villaResources;
    }

    private static ImmutableList<Villa> MapToVillas(IEnumerable<VillaResource> villaResources)
    {
        var villas = ImmutableList.Create<Villa>();
        foreach (var villaResource in villaResources)
            villas = villas.Add(MapToVilla(villaResource));
        return villas;
    }

    private static VillaResource MapVillaResource(Villa villa) =>
    new(
        Id: villa.Id,
        Name: villa.Name,
        Details: villa.Details,
        Rate: villa.Rate,
        Occupancy: villa.Occupancy,
        Sqft: villa.Sqft,
        ImageUrl: villa.ImageUrl,
        Amenity: villa.Amenity
        );
}