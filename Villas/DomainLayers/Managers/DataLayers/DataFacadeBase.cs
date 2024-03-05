using System.Collections.Immutable;
using Villas.DomainLayers.Models;
using Villas.DomainLayers.Managers.DataLayers.Managers;

namespace Villas.DomainLayers;

internal abstract class DataFacadeBase(string connectionString)
{
    private readonly string _connectionString = connectionString;

    private DataManagerBase _dataManager;

    protected DataManagerBase DataManager => _dataManager ??= new VillaDataManager(_connectionString);

    internal Task<ImmutableList<Villa>> GetVillasAsync() => GetVillasAsyncCore();
    protected abstract Task<ImmutableList<Villa>> GetVillasAsyncCore();

    internal Task<Villa> GetVillaByIdAsync(int villaId) => GetVillaByIdAsyncCore(villaId);
    protected abstract Task<Villa> GetVillaByIdAsyncCore(int villaId);

    internal Task<Villa> GetVillaByNameAsync(string villaName) => GetVillaByNameAsyncCore(villaName);
    protected abstract Task<Villa> GetVillaByNameAsyncCore(string villaName);

    internal Task<int> CreateVillaAsync(Villa villa) => CreateVillaAsyncCore(villa);
    protected abstract Task<int> CreateVillaAsyncCore(Villa villa);

    internal Task<int> UpdateVillaAsync(Villa villa) => UpdateVillaAsyncCore(villa);
    protected abstract Task<int> UpdateVillaAsyncCore(Villa villa);

    internal Task<int> DeleteVillaAsync(int villaId) => DeleteVillaAsyncCore(villaId);
    protected abstract Task<int> DeleteVillaAsyncCore(int villaId);
}
