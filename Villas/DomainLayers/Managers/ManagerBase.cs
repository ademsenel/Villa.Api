using System.Collections.Immutable;
using Villas.DomainLayers.Managers.ConfigurationProviders;
using Villas.DomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers;

internal abstract class ManagerBase(ServiceLocatorBase serviceLocator)
{
    private readonly ServiceLocatorBase _serviceLocator = serviceLocator;

    private ConfigurationProviderBase _configurationProvider;
    private ConfigurationProviderBase ConfigurationProvider => _configurationProvider ??= _serviceLocator.CreateConfigurationProvider();

    private DataFacadeBase _dataFacade;
    protected private DataFacadeBase DataFacade => _dataFacade ??= _serviceLocator.CreateDataFacade(ConfigurationProvider.GetSettingValue("DbConnectionString"));

    public Task<ImmutableList<Villa>> GetVillasAsync() => GetVillasAsyncCore();
    protected abstract Task<ImmutableList<Villa>> GetVillasAsyncCore();

    internal Task<Villa> GetVillasByNameAsync(string villaName) => GetVillasByNameAsyncCore(villaName);

    protected abstract Task<Villa> GetVillasByNameAsyncCore(string villaName);

    public Task<Villa> GetVillasByIdAsync(int id) => GetVillasByIdAsyncCore(id);
    protected abstract Task<Villa> GetVillasByIdAsyncCore(int id);

    public Task<int> CreateVillasAsync(Villa villa) => CreateVillasAsyncCore(villa);
    protected abstract Task<int> CreateVillasAsyncCore(Villa villa);

    public Task<int> UpdateVillasAsync(Villa villa) => UpdateVillasAsyncCore(villa);
    protected abstract Task<int> UpdateVillasAsyncCore(Villa villa);

    public Task<int> DeleteVillasAsync(int id) => DeleteVillasAsyncCore(id);
    protected abstract Task<int> DeleteVillasAsyncCore(int id);
}
