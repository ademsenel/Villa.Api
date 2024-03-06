namespace Villas.Api.Models;

public sealed record VillaResource(
    int Id,
    string Name,
    string Details,
    double Rate,
    int Occupancy,
    int Sqft,
    string ImageUrl,
    string Amenity
    );