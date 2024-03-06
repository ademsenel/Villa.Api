using System.Diagnostics.CodeAnalysis;

namespace Villas.DomainLayers.Models;

[ExcludeFromCodeCoverage]
public sealed record Villa(
    int Id,
    string Name,
    string Details,
    double Rate,
    int Sqft,
    int Occupancy,
    string ImageUrl,
    string Amenity
    );