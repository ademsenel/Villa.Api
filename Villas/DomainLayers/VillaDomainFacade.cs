using System.Collections.Immutable;
using Villas.DomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers;

public sealed class VillaDomainFacade : DomainFacadeBase
{
    public VillaDomainFacade() : base(new ServiceLocator()) { }
    internal VillaDomainFacade(ServiceLocatorBase serviceLocator) : base(serviceLocator) { }

    protected override Task<ImmutableList<Villa>> GetVillasAsyncCore() =>
        VillaManager.GetVillasAsync();

    protected override Task<Villa> GetVillaByIdAsyncCore(int villaId) =>
        VillaManager.GetVillasByIdAsync(villaId);

    protected override Task<Villa> GetVillaByNameAsyncCore(string villaName) =>
        VillaManager.GetVillasByNameAsync(villaName);

    protected override Task<int> CreateVillaAsyncCore(Villa villa) =>
        VillaManager.CreateVillasAsync(villa);

    protected override Task<int> UpdateVillaAsyncCore(Villa villa) =>
        VillaManager.UpdateVillasAsync(villa);

    protected override Task<int> DeleteVillaAsyncCore(int villaId) =>
        VillaManager.DeleteVillasAsync(villaId);
}