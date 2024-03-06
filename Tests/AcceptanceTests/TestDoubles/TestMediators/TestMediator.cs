using System.Collections.Immutable;
using Villas.DomainLayers.Models;

namespace AcceptanceTests.TestDoubles.TestMediators;

internal sealed class TestMediator
{
    public ImmutableList<Villa> VillasUnderTest { get; set; }
}
