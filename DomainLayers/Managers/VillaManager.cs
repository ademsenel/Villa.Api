using System.Collections.Immutable;
using Villas.DomainLayers.Exceptions;
using Villas.DomainLayers.Managers.ServiceLocators;
using Villas.DomainLayers.Managers.Validators;
using Villas.DomainLayers.Models;

namespace Villas.DomainLayers.Managers;

internal sealed class VillaManager(ServiceLocatorBase serviceLocator) : ManagerBase(serviceLocator)
{

    protected override Task<ImmutableList<Villa>> GetVillasAsyncCore() =>
        DataFacade.GetVillasAsync();

    protected override async Task<Villa> GetVillasByIdAsyncCore(int value)
    {
        VillaValidator.EnsureIdIsValid(nameof(Villa.Id), value);
        var villa = await DataFacade.GetVillaByIdAsync(value).ConfigureAwait(false);
        return villa ?? throw new VillaWithSpecifiedIdNotFoundException($"A {nameof(Villa)} with {nameof(Villa.Id)}: {value} was not found.");
    }

    protected override async Task<Villa> GetVillasByNameAsyncCore(string villaName)
    {
        VillaValidator.EnsureNameIsValid(nameof(Villa.Name), villaName);
        var villa = await DataFacade.GetVillaByNameAsync(villaName).ConfigureAwait(false);
        return villa ?? throw new VillaWithSpecifiedNameNotFoundException($"A {nameof(Villa)} with {nameof(Villa.Name)}: {villaName} was not found.");
    }

    protected override async Task<int> CreateVillasAsyncCore(Villa villa)
    {
        VillaValidator.EnsureVillaIsValid(villa);
        return await DataFacade.CreateVillaAsync(villa).ConfigureAwait(false);
    }

    protected override async Task<int> UpdateVillasAsyncCore(Villa villa)
    {
        VillaValidator.EnsureIdIsValid(nameof(Villa.Id), villa.Id);
        VillaValidator.EnsureVillaIsValid(villa);
        return await DataFacade.UpdateVillaAsync(villa).ConfigureAwait(false);
    }

    protected override async Task<int> DeleteVillasAsyncCore(int value)
    {
        VillaValidator.EnsureIdIsValid(nameof(Villa.Id), value);
        return await DataFacade.DeleteVillaAsync(value).ConfigureAwait(false);
    }
}
