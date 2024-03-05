using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Villas.Api.Models;
using Villas.Api.Repositories;
#pragma warning disable IDE0060 // Remove unused parameter

namespace Villas.Api.Controllers;

[Route("Api/Villas")]
[ApiController]
public class VillasController(RepositoryBase repository) : ControllerBase
{
    private readonly RepositoryBase _repository = repository;


    [HttpGet]
    public async Task<IEnumerable<VillaResource>> GetVillas()
    {
        return await GetVillasCoreAsync().ConfigureAwait(false);
    }

    [HttpGet("Id/{id:int}", Name = "Id")]
    public async Task<VillaResource> GetVillaById(int id)
    {
        return await GetVillaByIdCoreAsync(id).ConfigureAwait(false);
    }

    [HttpGet("Name/{name}", Name = "Name")]
    public async Task<VillaResource> GetVillaByName(string name)
    {
        return await GetVillaByNameCoreAsync(name).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<VillaResource>> CreateVilla(VillaResource villaResource)
    {
        var result = await CreateVillaCoreAsync(villaResource).ConfigureAwait(false);
        var villa = new VillaResource(result, villaResource.Name, villaResource.Details, villaResource.Rate, villaResource.Occupancy, villaResource.Sqft, villaResource.ImageUrl, villaResource.Amenity);
        return CreatedAtRoute("Id", new { Id = result }, villa);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task DeleteVilla(int id)
    {
        await DeleteVillaCoreAsync(id).ConfigureAwait(false);
    }

    [HttpPut("Edit/{id:int}")]
    public async Task UpdateVilla(int id, VillaResource villaResource)
    {
        await UpdateVillaCoreAsync(villaResource).ConfigureAwait(false);
    }

    #region Protected Method
    [ExcludeFromCodeCoverage]
    protected virtual Task<ImmutableList<VillaResource>> GetVillasCoreAsync() =>
        _repository.GetAllAsync();

    [ExcludeFromCodeCoverage]
    protected virtual Task<VillaResource> GetVillaByIdCoreAsync(int id) =>
        _repository.GetByIdAsync(id);

    [ExcludeFromCodeCoverage]
    protected virtual Task<VillaResource> GetVillaByNameCoreAsync(string name) =>
        _repository.GetByNameAsync(name);

    [ExcludeFromCodeCoverage]
    protected virtual Task<int> CreateVillaCoreAsync(VillaResource villaResource) =>
        _repository.CreateAsync(villaResource);

    [ExcludeFromCodeCoverage]
    protected virtual async Task DeleteVillaCoreAsync(int id) =>
        await _repository.DeleteAsync(id).ConfigureAwait(false);

    [ExcludeFromCodeCoverage]
    protected virtual async Task UpdateVillaCoreAsync(VillaResource villaResource) =>
        await _repository.UpdateAsync(villaResource).ConfigureAwait(false);
    #endregion
}
