using System.Collections.Immutable;
using Villas.Api.Models;
using Villas.DomainLayers.Models;

namespace Villas.Api.Mappers;
internal static class Mapper
{
    internal static ImmutableList<VillaResource> MapToVillaResource(ImmutableList<Villa> villas)
    {
        var villaResources = ImmutableList.Create<VillaResource>();
        foreach (var villa in villas)
            villaResources = villaResources.Add(MapToVillaResource(villa));
        return villaResources;
    }

    internal static VillaResource MapToVillaResource(Villa villa) =>
        new(
            Id: villa.Id,
            Name: villa.Name,
            Details: villa.Details,
            Rate: villa.Rate,
            Occupancy: villa.Occupancy,
            Sqft: villa.Sqft,
            ImageUrl: villa.ImageUrl,
            Amenity: villa.Amenity
            );

    internal static Villa MapToVilla(VillaResource villaResource) =>
        new(
            Id: villaResource.Id,
            Name: villaResource.Name,
            Details: villaResource.Details,
            Rate: villaResource.Rate,
            Occupancy: villaResource.Occupancy,
            Sqft: villaResource.Sqft,
            ImageUrl: villaResource.ImageUrl,
            Amenity: villaResource.Amenity
            );
}


