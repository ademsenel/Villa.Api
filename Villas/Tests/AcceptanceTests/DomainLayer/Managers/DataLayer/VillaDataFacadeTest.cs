using AcceptanceTests.TestDataGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Shared;
using Villas.DomainLayers.Managers.DataLayers.Managers;
using Villas.DomainLayers.Models;
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace AcceptanceTests.VillaDomainLayers.Managers.DataLayer;

[TestClass]
public sealed class VillaDataFacadeTest : VillaDataFacadeTestBase
{
    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task GetVillasAsync_WhenCalledReturnAllVilla_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVillas = _villasInDb;

        // Act
        var actualVillas = await dataFacade.GetVillasAsync().ConfigureAwait(false);

        // Assert
        VillaAssertions.AssertVillasAreEqual(expectedVillas, actualVillas);
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task GetVillaByIdAsync_WhenCalledWithValidId_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVillaId = _villasInDb[0].Id;
        Villa expectedVilla = _villasInDb.Find(x => x.Id == expectedVillaId);

        // Act
        Villa actualVilla = await dataFacade.GetVillaByIdAsync(expectedVillaId).ConfigureAwait(false);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task GetVillaByNameAsync_WhenCalledWithValidName_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVillaName = _villasInDb[0].Name;
        Villa expectedVilla = _villasInDb.Find(x => x.Name == expectedVillaName);

        // Act
        Villa actualVilla = await dataFacade.GetVillaByNameAsync(expectedVillaName).ConfigureAwait(false);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task CreateVillaAsync_WhenCalledWithAValidVillaNonExistent_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVilla = RandomGenerator.GenerateRandomVillas(1).Single();

        // Act
        var actualVillaId = await dataFacade.CreateVillaAsync(expectedVilla).ConfigureAwait(false);
        var actualVilla = (await VillaTestDataGenerator.GetVillasAsync(_connectionString).ConfigureAwait(false))
            .Find(x => x.Id == actualVillaId);

        // Assert
        VillaAssertions.AssertVillasAreEqual([expectedVilla], [actualVilla]);
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task CreateVillaAsync_WhenCalledInvalidVillaThrownException_ShouldThrow()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVilla = new Villa(1, null!, null!, 2, 2, 2, null!, null!);
        try
        {
            // Act
            var actualVillaId = await dataFacade.CreateVillaAsync(expectedVilla).ConfigureAwait(false);

            Assert.Fail($"We were expecting a {nameof(SqlDbException)} to be thrown, but no exception was thrown.");
        }
        catch (SqlDbException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"Unknow Db Error");
        }
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task UpdateVillaAsync_WhenCalledWithAValidVillaExistent_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVilla = RandomGenerator.GenerateRandomVillas(1)[0];
        var expectedVillaId = await VillaTestDataGenerator.CreateVillaAsync(_connectionString, expectedVilla).ConfigureAwait(false);
        var updatedVilla  = new Villa(expectedVillaId, "UpdatedName", "UpdatedDetails", 2, 2, 2, "https://www.image.com/Updatedimage.png", "UpdatedAmenity");

        // Act
        var actualVillaId = await dataFacade.UpdateVillaAsync(updatedVilla).ConfigureAwait(false);

        // Assert
        Assert.AreEqual(expectedVillaId, actualVillaId);
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task UpdateVillaAsync_WhenCalledThrownException_ShouldThrow()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var updatedVilla = new Villa(2, null!, null!, 2, 2, 2, null!, null!);

        try
        {
            // Act
            var actualVillaId = await dataFacade.UpdateVillaAsync(updatedVilla).ConfigureAwait(false);

            Assert.Fail($"We were expecting a {nameof(SqlDbException)} to be thrown, but no exception was thrown.");
        }
        catch (SqlDbException e)
        {
            // Assert
            StringAssert.Contains(e.Message, $"Unknow Db Error");
        }
    }

    [TestMethod]
    [TestCategory("Edge Case Test")]
    public async Task DeleteVillaAsync_WhenCalledWithAValidVilla_Succeed()
    {
        // Arrange
        var dataFacade = CreateDataFacade();
        var expectedVilla = RandomGenerator.GenerateRandomVillas(1)[0];
        var expectedVillaId = await VillaTestDataGenerator.CreateVillaAsync(_connectionString, expectedVilla).ConfigureAwait(false);

        // Act
        var actualVillaId = await dataFacade.DeleteVillaAsync(expectedVillaId).ConfigureAwait(false);


        // Assert
        Assert.AreEqual(expectedVillaId, actualVillaId);
    }
}