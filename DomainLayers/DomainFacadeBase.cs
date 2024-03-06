using System.Collections.Immutable;
using Villas.DomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers.Managers;
using Villas.DomainLayers.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AcceptanceTests")]
[assembly: InternalsVisibleTo("ClassTests")]
[assembly: InternalsVisibleTo("Testing.Shared")]
[assembly: InternalsVisibleTo("ControllerTests")]
[assembly: InternalsVisibleTo("EndToEndIntegrationTests")]

namespace Villas.DomainLayers;

public abstract class DomainFacadeBase
{
    private readonly ServiceLocatorBase _serviceLocator;

    private ManagerBase _villaManager;
    protected private ManagerBase VillaManager => _villaManager ??= _serviceLocator.CreateManager();

    protected private DomainFacadeBase(ServiceLocatorBase serviceLocator) => _serviceLocator = serviceLocator;

    public Task<ImmutableList<Villa>> GetVillasAsync() => GetVillasAsyncCore();
    protected abstract Task<ImmutableList<Villa>> GetVillasAsyncCore();

    public Task<Villa> GetVillaByIdAsync(int villaId) => GetVillaByIdAsyncCore(villaId);
    protected abstract Task<Villa> GetVillaByIdAsyncCore(int villaId);

    public Task<Villa> GetVillaByNameAsync(string villaName) => GetVillaByNameAsyncCore(villaName);
    protected abstract Task<Villa> GetVillaByNameAsyncCore(string villaName);

    public Task<int> CreateVillaAsync(Villa villa) => CreateVillaAsyncCore(villa);
    protected abstract Task<int> CreateVillaAsyncCore(Villa villa);

    public Task<int> UpdateVillaAsync(Villa villa) => UpdateVillaAsyncCore(villa);
    protected abstract Task<int> UpdateVillaAsyncCore(Villa villa);

    public Task<int> DeleteVillaAsync(int villaId) => DeleteVillaAsyncCore(villaId);
    protected abstract Task<int> DeleteVillaAsyncCore(int villaId);
}
