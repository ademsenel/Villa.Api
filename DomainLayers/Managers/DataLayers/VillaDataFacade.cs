using System.Collections.Immutable;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.DataLayers;

internal sealed class VillaDataFacade(string connectionString) : DataFacadeBase(connectionString)
{
    protected override Task<ImmutableList<Villa>> GetVillasAsyncCore() =>
        DataManager.GetVillasAsync();

    protected override Task<Villa> GetVillaByIdAsyncCore(int villaId) =>
        DataManager.GetVillaByIdAsync(villaId, nameof(Villa.Id));

    protected override Task<Villa> GetVillaByNameAsyncCore(string villaName) =>
        DataManager.GetVillaByNameAsync(villaName, nameof(Villa.Name));

    protected override Task<int> CreateVillaAsyncCore(Villa villa) =>
        DataManager.CreateVillaAsync(villa);

    protected override Task<int> UpdateVillaAsyncCore(Villa villa) =>
        DataManager.UpdateVillaAsync(villa);

    protected override Task<int> DeleteVillaAsyncCore(int villaId) =>
        DataManager.DeleteVillaAsync(villaId);

}
