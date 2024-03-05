using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;
using Testing.Shared;
using Testing.Shared.TestingHelpers;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Models;
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace AcceptanceTests.VillaDomainLayers;

[TestClass]
public sealed class VillaDomainFacadeTests : VillaDomainFacadeTestsBase
{
    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task GetVillasAsync_WhenCalledReturnAllVilla_Succeed()
    {
        var (domainFacade, testMediator) = CreateDomainFacade();
        testMediator.VillasUnderTest = RandomGenerator.GenerateRandomVillas(VillaCount);

        var expectedVillas = testMediator.VillasUnderTest;

        // Act
        var actualVillas = await domainFacade.GetVillasAsync().ConfigureAwait(false);

        // Assert
        VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task GetVillaByIdAsync_WhenCalledWithValidIdShouldReturn_Succeed()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var expectedVilla = new Villa(2, "Name", "Details", 1.5, 2, 2, "https://www.ImageUrl.com/image.png", "Ameninty");
        testMediator.VillasUnderTest = [expectedVilla];
        var expectedVillaId = expectedVilla.Id;

        // Act
        Villa actualVilla = await domainFacade.GetVillaByIdAsync(expectedVillaId);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task GetVillaByIdAsync_WhenCalledWithAInvalidVillaIdNotExistent_ShouldThrow()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var nonExistentVillaId = 1053125874;
        testMediator.VillasUnderTest = RandomGenerator.GenerateRandomVillas(VillaCount);
        try
        {
            // Act
            _ = await domainFacade.GetVillaByIdAsync(nonExistentVillaId);
            Assert.Fail($"We were expecting a {nameof(VillaWithSpecifiedIdNotFoundException)} to be thrown, but no exception was thrown.");
        }
        catch (VillaWithSpecifiedIdNotFoundException e)
        {
            StringAssert.Contains(e.Message, $"{nameof(Villa.Id)}: {nonExistentVillaId}");
        }
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task GetVillaByNameAsync_WhenCalledWithValidVillaNameShouldReturn_Succeed()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var expectedVilla = new Villa(2, "Name", "Details", 1.5, 2, 2, "https://www.ImageUrl.com/image.png", "Ameninty");
        testMediator.VillasUnderTest = [expectedVilla];
        var expectedVillaName = expectedVilla.Name;

        // Act
        Villa actualVilla = await domainFacade.GetVillaByNameAsync(expectedVillaName);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task GetVillaByNameAsync_WhenCalledWithInValidVillaName_ShouldThrow()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var villaName = "InvalidName";
        testMediator.VillasUnderTest = RandomGenerator.GenerateRandomVillas(VillaCount);
        try
        {
            // Act
            _ = await domainFacade.GetVillaByNameAsync(villaName);
        }
        catch (VillaWithSpecifiedNameNotFoundException e)
        {
            // Assert
            AssertEx.EnsureExceptionMessageContains(e, $"with {nameof(Villa.Name)}: {villaName} was not found.");
        }
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task CreateVillaAsync_WhenCalledWithAValidVillaNonExistent_Succeed()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var expectedVilla = new Villa(2, "Name", "Details", 1.5, 2, 2, "https://www.ImageUrl.com/image.png", "Ameninty");
        testMediator.VillasUnderTest = [expectedVilla];

        // Act
        var actualVillaId = await domainFacade.CreateVillaAsync(expectedVilla);
        Villa actualVilla = testMediator.VillasUnderTest.Find(x => x.Id == actualVillaId);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task UpdateVillaAsync_WhenCalledWithAValidVilla_Succeed()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var expectedVilla = new Villa(2, "Name", "Details", 1.5, 2, 2, "https://www.ImageUrl.com/image.png", "Ameninty");
        testMediator.VillasUnderTest = [expectedVilla];

        // Act
        var actualVillaId = await domainFacade.UpdateVillaAsync(expectedVilla);
        Villa actualVilla = testMediator.VillasUnderTest.Find(x => x.Id == actualVillaId);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Acceptance Test")]
    public async Task DeleteVillaAsync_WhenCalledWithAValidVilla_Succeed()
    {
        // Arrange
        var (domainFacade, testMediator) = CreateDomainFacade();
        var expectedVilla = new Villa(2, "Name", "Details", 1.5, 2, 2, "https://www.ImageUrl.com/image.png", "Ameninty");
        testMediator.VillasUnderTest = [expectedVilla];

        // Act
        var actualVillaId = await domainFacade.DeleteVillaAsync(expectedVilla.Id);

        // Assert
        Assert.AreEqual(expectedVilla.Id, actualVillaId);
    }
}
