using System.Diagnostics.CodeAnalysis;
using Villas.DomainLayers.Models;

namespace Testing.Shared.EqualityComparers;

[ExcludeFromCodeCoverage]
internal sealed class VillaEqualityComparer : IEqualityComparer<Villa>
{
    public bool Equals([NotNullWhen(true)] Villa x, [NotNullWhen(true)] Villa y) =>
        x != null && y != null
        && x.Name == y.Name
        && x.Details == y.Details
        && x.Rate == y.Rate
        && x.Sqft == y.Sqft
        && x.Occupancy == y.Occupancy
        && x.ImageUrl == y.ImageUrl
        && x.Amenity == y.Amenity;

    public int GetHashCode([DisallowNull] Villa obj) =>
        HashCode.Combine(
            obj.Name,
            obj.Details,
            obj.Rate,
            obj.Sqft,
            obj.Occupancy,
            obj.ImageUrl,
            obj.Amenity
            );
}