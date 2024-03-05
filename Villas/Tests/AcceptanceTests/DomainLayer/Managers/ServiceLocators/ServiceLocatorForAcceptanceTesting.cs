using AcceptanceTests.TestDoubles.TestMediators;
using AcceptanceTests.TestDoubles.TestSpies.Managers.DataLayers;
using Villas.DomainLayers;
using Villas.DomainLayers.Managers;
using Villas.DomainLayers.Managers.ConfigurationProviders;
using Villas.DomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers.Models;

namespace AcceptanceTests.VillaDomainLayers.Managers.ServiceLocators;

internal sealed class ServiceLocatorForAcceptanceTesting(TestMediator testMediator) : ServiceLocatorBase
{
    private readonly TestMediator _testMediator = testMediator;

    protected override DataFacadeBase CrateDataFacadeCore(string connectionString) =>
        new VillaDataFacadeSpy(_testMediator);

    protected override ConfigurationProviderBase CreateConfigurationProviderCore() =>
        new ConfigurationProvider($"{nameof(Villa)}ApiAppSettings:");

    protected override ManagerBase CreateManagerCore() =>
        new VillaManager(this);
}