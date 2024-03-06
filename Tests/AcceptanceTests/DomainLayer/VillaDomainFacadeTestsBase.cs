using AcceptanceTests.TestDoubles.TestMediators;
using AcceptanceTests.VillaDomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers;

namespace AcceptanceTests.VillaDomainLayers;

public abstract class VillaDomainFacadeTestsBase
{
    protected const int VillaCount = 30;

    private protected static (VillaDomainFacade domainFacade, TestMediator testMediator) CreateDomainFacade()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        var testMediator = new TestMediator();
        var serviceLocatorForAcceptanceTesting = new ServiceLocatorForAcceptanceTesting(testMediator);
        return (new VillaDomainFacade(serviceLocatorForAcceptanceTesting), testMediator);
    }
}
