using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared.TestingHelpers;
using Villas.DomainLayers.Models;

namespace Testing.Shared;

public static class RandomGenerator
{
    public static ImmutableList<Villa> GenerateRandomVillas(int count)
    {
        var randomVillas = ImmutableList.Create<Villa>();
        for (int i = 0; i < count; i++)
        {
            var villa = CreateVilla();
            randomVillas = randomVillas.Add(villa);
        }
        return [.. randomVillas];
    }

    public static Villa CreateVillaById(int villaId) =>
        CreateVilla(villaId);

    private static Villa CreateVilla(int villaId = 0)
    {
        return new(
            Id: villaId,
            Name: RandomStringGenerator.GetRandomACIIString(50),
            Details: RandomStringGenerator.GetRandomACIIString(100),
            Rate: RandomStringGenerator.GetRandomDouble(),
            Sqft: RandomStringGenerator.GetRandomInteger(100, 200),
            Occupancy: RandomStringGenerator.GetRandomInteger(1, 100),
            ImageUrl: RandomStringGenerator.GetRandomUrl(),
            Amenity: RandomStringGenerator.GetRandomACIIString(50)
            );
    }
}