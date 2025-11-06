using AutoFixture;

using Bogus;

namespace DataGenerator.Configuration;

internal static class FixtureFactory
{
    public static int DefaultSeed => int.TryParse(Environment.GetEnvironmentVariable("TEST_SEED"), out var s) ? s : 123456;

    public static IFixture Create(string locale = "ru", int? seed = null, int recursionDepth = 3, Action<Faker>? configureFaker = null)
    {
        var usedSeed = seed ?? DefaultSeed;
        Randomizer.Seed = new Random(usedSeed);

        var fixture = new Fixture();

        var random = new Random(usedSeed);
        fixture.Register(() => random);

        var faker = new Faker(locale) { Random = new Randomizer(usedSeed) };
        configureFaker?.Invoke(faker);

        fixture.Customize(new DataGenerationCustomization(
            locale,
            faker => { faker.Random = new Randomizer(usedSeed); configureFaker?.Invoke(faker); },
            recursionDepth));

        return fixture;
    }
}
