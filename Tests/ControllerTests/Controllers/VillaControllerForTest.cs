using System.Collections.Immutable;
using Villas.Api.Controllers;
using Villas.Api.Models;

namespace ControllerTests.Controllers;

public class VillaControllerForTest : VillasController
{
    private readonly ImmutableList<VillaResource> _villas = default!;
    private readonly VillaResource _villa = default!;
    private readonly Exception _exception = default!;

    public VillaControllerForTest(ImmutableList<VillaResource> villas) : base(default!) =>
        _villas = villas;

    public VillaControllerForTest(VillaResource villa) : base(default!) =>
        _villa = villa;

    public VillaControllerForTest(Exception exception) : base(default!) =>
        _exception = exception;

    protected override Task<ImmutableList<VillaResource>> GetVillasCoreAsync() =>
        _exception != null ? throw _exception : Task.FromResult(_villas);

    protected override Task<VillaResource> GetVillaByIdCoreAsync(int id) =>
        Task.FromResult(_villa);

    protected override Task<VillaResource> GetVillaByNameCoreAsync(string name) =>
        Task.FromResult(_villa);

    protected override Task<int> CreateVillaCoreAsync(VillaResource villaResource) =>
        _exception != null ? throw _exception : Task.FromResult(0);

    protected override Task UpdateVillaCoreAsync(VillaResource villaResource) =>
        _exception != null ? throw _exception : Task.FromResult(0);

    protected override Task DeleteVillaCoreAsync(int id) =>
        _exception != null ? throw _exception : Task.FromResult(0);
}
