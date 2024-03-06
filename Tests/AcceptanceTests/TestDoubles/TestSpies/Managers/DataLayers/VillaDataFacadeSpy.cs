using System.Collections.Immutable;
using Villas.DomainLayers.Models;
using Villas.DomainLayers;
using AcceptanceTests.TestDoubles.TestMediators;

namespace AcceptanceTests.TestDoubles.TestSpies.Managers.DataLayers;

internal sealed class VillaDataFacadeSpy(TestMediator testMediator) : DataFacadeBase(default!)
{
    private readonly TestMediator _testMediator = testMediator;

    protected override Task<ImmutableList<Villa>> GetVillasAsyncCore() =>
       Task.Run(() => _testMediator.VillasUnderTest);

    protected override Task<Villa> GetVillaByIdAsyncCore(int villaId) =>
        Task.Run(() => _testMediator.VillasUnderTest.Find(x => x.Id == villaId));

    protected override Task<Villa> GetVillaByNameAsyncCore(string villaName) =>
        Task.Run(() => _testMediator.VillasUnderTest.Find(x => x.Name == villaName));

    protected override Task<int> CreateVillaAsyncCore(Villa villa) =>
        Task.Run(() => villa.Id);

    protected override Task<int> UpdateVillaAsyncCore(Villa villa) =>
        Task.Run(() => villa.Id);

    protected override Task<int> DeleteVillaAsyncCore(int villaId) =>
        Task.Run(() => villaId);
}