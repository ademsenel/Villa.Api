using AcceptanceTests.VillaDomainLayers.Managers.ServiceLocators;
using System.Collections.Immutable;
using Villas.DomainLayers.Managers.DataLayers;
using Villas.DomainLayers.Models;
using Villas.DomainLayers;
using AcceptanceTests.TestDataGenerators;

namespace AcceptanceTests.VillaDomainLayers.Managers.DataLayer;


public abstract class VillaDataFacadeTestBase
{
    protected private ImmutableList<Villa> _villasInDb = [];
    protected private readonly string _connectionString;

    protected VillaDataFacadeTestBase()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        _connectionString = new ServiceLocatorForAcceptanceTesting(null!).CreateConfigurationProvider().GetSettingValue("DbConnectionString");
        _villasInDb = VillaTestDataGenerator.GetVillasAsync(_connectionString).GetAwaiter().GetResult();
    }

    protected private DataFacadeBase CreateDataFacade() =>
        new VillaDataFacade(_connectionString);
}