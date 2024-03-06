using System.Collections.Immutable;
using Villas.Api.Models;
using Villas.DomainLayers;

namespace Villas.Api.Repositories;

public abstract class RepositoryBase(VillaDomainFacade domainFacade)
{
    protected private readonly VillaDomainFacade _domainFacade = domainFacade;

    internal Task<ImmutableList<VillaResource>> GetAllAsync() => GetAllCoreAsync();
    protected abstract Task<ImmutableList<VillaResource>> GetAllCoreAsync();

    internal Task<VillaResource> GetByIdAsync(int villaId) => GetByIdCoreAsync(villaId);
    protected abstract Task<VillaResource> GetByIdCoreAsync(int villaId);

    internal Task<VillaResource> GetByNameAsync(string villaName) => GetByNameCoreAsync(villaName);
    protected abstract Task<VillaResource> GetByNameCoreAsync(string villaName);

    internal Task<int> CreateAsync(VillaResource villaResource) => CreateCoreAsync(villaResource);
    protected abstract Task<int> CreateCoreAsync(VillaResource villaResource);

    internal Task DeleteAsync(int villaId) => DeleteAsyncCore(villaId);
    protected abstract Task DeleteAsyncCore(int villaId);

    internal Task UpdateAsync(VillaResource villaResource) => UpdateAsyncCore(villaResource);
    protected abstract Task UpdateAsyncCore(VillaResource villaResource);
}
