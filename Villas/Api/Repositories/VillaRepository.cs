using System.Collections.Immutable;
using Villas.Api.Mappers;
using Villas.Api.Models;
using Villas.DomainLayers;

namespace Villas.Api.Repositories;

internal sealed class VillaRepository(VillaDomainFacade domainFacade) : RepositoryBase(domainFacade)
{
    protected override Task<int> CreateCoreAsync(VillaResource villaResource) =>
        _domainFacade.CreateVillaAsync(Mapper.MapToVilla(villaResource));

    protected override Task DeleteAsyncCore(int villaId) =>
        _domainFacade.DeleteVillaAsync(villaId);

    protected override async Task<ImmutableList<VillaResource>> GetAllCoreAsync() =>
        Mapper.MapToVillaResource(await _domainFacade.GetVillasAsync().ConfigureAwait(false));

    protected override async Task<VillaResource> GetByIdCoreAsync(int villaId) =>
        Mapper.MapToVillaResource(await _domainFacade.GetVillaByIdAsync(villaId).ConfigureAwait(false));

    protected override async Task<VillaResource> GetByNameCoreAsync(string villaName) =>
        Mapper.MapToVillaResource(await _domainFacade.GetVillaByNameAsync(villaName).ConfigureAwait(false));

    protected override Task UpdateAsyncCore(VillaResource villaResource) =>
        _domainFacade.UpdateVillaAsync(Mapper.MapToVilla(villaResource));
}
