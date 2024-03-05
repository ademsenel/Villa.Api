using Villas.DomainLayers.Managers.ConfigurationProviders;
using Villas.DomainLayers.Managers.DataLayers;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers.ServiceLocators;

internal sealed class ServiceLocator : ServiceLocatorBase
{
    protected override DataFacadeBase CrateDataFacadeCore(string connectionString) =>
        new VillaDataFacade(connectionString);

    protected override ConfigurationProviderBase CreateConfigurationProviderCore() =>
        new ConfigurationProvider($"{nameof(Villa)}ApiAppSettings:");

    protected override ManagerBase CreateManagerCore() =>
        new VillaManager(this);
}
