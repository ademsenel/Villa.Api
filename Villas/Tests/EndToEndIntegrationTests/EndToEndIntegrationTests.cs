using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;
using Testing.Shared;
using Villas.Api.Models;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Models;

#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace EndToEndIntegrationTests;

[TestClass]
public sealed class EndToEndIntegrationTests : EndToEndIntegrationTestsBase
{
    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillasAsync_WhenOperatingNormally_ShouldSucceed()
    {        
        // Act
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        var httpResponseMessage = await _httpClient.GetAsync("Api/Villas").ConfigureAwait(false);

        // Assert
        await EnsureSuccess(httpResponseMessage).ConfigureAwait(false);
        var actualVillaResource = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VillaResource>>().ConfigureAwait(false);
        Assert.IsTrue(actualVillaResource!.Any());
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillaByIdAsync_WhenProvideWithAValidAndExistingId_ShouldReturnVillaForId()
    {
        // Act 
        var validId = 3;
        var httpResponseMessage = await _httpClient.GetAsync($"Api/Villas/Id/{validId}").ConfigureAwait(false);

        // Assert
        await EnsureSuccess(httpResponseMessage).ConfigureAwait(false);
        var actualVillaResource = await httpResponseMessage.Content.ReadFromJsonAsync<VillaResource>().ConfigureAwait(false);
        Assert.AreEqual(validId, actualVillaResource.Id);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillaByIdAsync_WhenProvideWithAnInvalidId_ShouldReturnVHttpError()
    {
        // Arrange 
        var inValidId = -1;
        var invalidIdException = new VillaValidationException($"{nameof(Villa)} {nameof(Villa.Id)} must be a valid {nameof(Villa.Id)} and can not be less than {inValidId}.");

        // Act
        var httpResponseMessage = await _httpClient.GetAsync($"Api/Villas/Id/{inValidId}").ConfigureAwait(false);

        // Assert
        await EnsureErrorResponseIsCorrect(httpResponseMessage, HttpStatusCode.BadRequest, invalidIdException).ConfigureAwait(false);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillaByIdAsync_WhenProvideWithAnInvalidAndNotExistingId_ShouldReturnVHttpError()
    {
        // Arrange 
        var inValidId = 6548465;
        var invalidIdException = new VillaWithSpecifiedIdNotFoundException($"A {nameof(Villa)} with {nameof(Villa.Id)}: {inValidId} was not found.");

        // Act
        var httpResponseMessage = await _httpClient.GetAsync($"Api/Villas/Id/{inValidId}").ConfigureAwait(false);

        // Assert
        await EnsureErrorResponseIsCorrect(httpResponseMessage, HttpStatusCode.NotFound, invalidIdException).ConfigureAwait(false);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillaByNameAsync_WhenProvideWithAValidAndExistingName_ShouldReturnVillaForName()
    {
        // Act 
        var validName = "Diamond Villa";
        var httpResponseMessage = await _httpClient.GetAsync($"Api/Villas/Name/{validName}").ConfigureAwait(false);

        // Assert
        await EnsureSuccess(httpResponseMessage).ConfigureAwait(false);
        var actualVillaResource = await httpResponseMessage.Content.ReadFromJsonAsync<VillaResource>().ConfigureAwait(false);
        Assert.AreEqual(validName, actualVillaResource.Name);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task GetVillaByNamedAsync_WhenProvideWithAnInvalidAndNotExistingName_ShouldReturnVHttpError()
    {
        // Arrange 
        var inValidName = "inValidName";
        var invalidIdException = new VillaWithSpecifiedNameNotFoundException($"A {nameof(Villa)} with {nameof(Villa.Name)}: {inValidName} was not found.");

        // Act
        var httpResponseMessage = await _httpClient.GetAsync($"Api/Villas/Name/{inValidName}").ConfigureAwait(false);

        // Assert
        await EnsureErrorResponseIsCorrect(httpResponseMessage, HttpStatusCode.NotFound, invalidIdException).ConfigureAwait(false);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task CreateVilla_WhenProvidedWithAValidVillaResource_ShouldCreateVilla()
    {
        // Arrange
        var villa = RandomGenerator.GenerateRandomVillas(1).Single();
        var villaResource =
            new VillaResource(
                0,
                villa.Name,
                villa.Details,
                villa.Rate,
                villa.Occupancy,
                villa.Sqft,
                villa.ImageUrl,
                villa.Amenity
                );

        // Act
        var httpResponseMessage = await _httpClient.PostAsJsonAsync("Api/Villas", villaResource).ConfigureAwait(false);

        var villaName = (await httpResponseMessage.Content.ReadFromJsonAsync<VillaResource>().ConfigureAwait(false)).Name;

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, httpResponseMessage.StatusCode, $$"""The Response content is: {{await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)}} $"The Villa that was used is: {{nameof(Villa.Name)}}: {{villa.Name}}, {{nameof(Villa.Details)}}: {{villa.Details}}, {{nameof(Villa.Rate)}}: {{villa.Rate}}, {{nameof(Villa.Occupancy)}}: {{villa.Occupancy}}, {{nameof(Villa.Sqft)}}: {{villa.Sqft}}, {{nameof(Villa.ImageUrl)}}: {{villa.ImageUrl}}, {{nameof(Villa.Amenity)}}: {{villa.Amenity}}.""");
        Assert.AreEqual(villa.Name, villaName);
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task UpdateVillaAsync_WhenProvidedWithAValidVilla_ShouldUpdateVilla()
    {
        // Arrange
        var expectedHttpResponseMessage = await _httpClient.GetAsync($"Api/Villas").ConfigureAwait(false);
        await EnsureSuccess(expectedHttpResponseMessage).ConfigureAwait(false);
        var createdVilla = (await expectedHttpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<VillaResource>>().ConfigureAwait(false)).LastOrDefault();
       
        var updatedVillaResource = new VillaResource(createdVilla.Id, "UpdatedVilla.Name", "UpdatedVilla.Details", 2, 2, 2, "UpdatedVilla.ImageUrl", "UpdatedVilla.Amenity");

        // Act
        var httpResponseMessage = await _httpClient.PutAsJsonAsync($"Api/Villas/Edit/{createdVilla.Id}", updatedVillaResource).ConfigureAwait(false);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode, $$"""The Response content is: {{await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)}} $"The Villa that was used is: Name: {{createdVilla.Name}}, Details: {{createdVilla.Details}}, Rate: {{createdVilla.Rate}}, Occupancy: {{createdVilla.Occupancy}}, Sqft: {{createdVilla.Sqft}}, ImageUrl: {{createdVilla.ImageUrl}}, Amenity: {{createdVilla.Amenity}}.""");
    }

    [TestMethod, TestCategory("EndToEndIntegration Test")]
    public async Task DeleteVillaAsync_WhenProvidedWithAValidVilla_ShouldDeleteVilla()
    {
        // Arrange
        var villa = RandomGenerator.GenerateRandomVillas(1)[0];
        var villasResponseHeader = await _httpClient.GetAsync("Api/Villas").ConfigureAwait(false);
        var actualVillaResource = await villasResponseHeader.Content.ReadFromJsonAsync<IEnumerable<VillaResource>>().ConfigureAwait(false);
        var villaId = actualVillaResource.LastOrDefault().Id;

        // Act
        var httpResponseMessage = await _httpClient.DeleteAsync($"Api/Villas/Delete/{villaId}").ConfigureAwait(false);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode, $$"""The Response content is: {{await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)}} $"The Villa that was used is: Name: {{villa.Name}}, Details: {{villa.Details}}, Rate: {{villa.Rate}}, Occupancy: {{villa.Occupancy}}, Sqft: {{villa.Sqft}}, ImageUrl: {{villa.ImageUrl}}, Amenity: {{villa.Amenity}}.""");
    }
}
