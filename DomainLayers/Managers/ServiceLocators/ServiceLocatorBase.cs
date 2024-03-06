using Villas.DomainLayers.Managers.ConfigurationProviders;

namespace Villas.DomainLayers.Managers.ServiceLocators;

internal abstract class ServiceLocatorBase
{
    internal ManagerBase CreateManager() => CreateManagerCore();
    protected abstract ManagerBase CreateManagerCore();

    internal DataFacadeBase CreateDataFacade(string connectionString) => CrateDataFacadeCore(connectionString);
    protected abstract DataFacadeBase CrateDataFacadeCore(string connectionString);

    internal ConfigurationProviderBase CreateConfigurationProvider() => CreateConfigurationProviderCore();
    protected abstract ConfigurationProviderBase CreateConfigurationProviderCore();
}
