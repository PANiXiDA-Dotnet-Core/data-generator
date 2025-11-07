using AutoFixture;
using AutoFixture.Dsl;

using Bogus;

using DataGenerator.Configuration;
using DataGenerator.Core;
using DataGenerator.Helpers;

namespace DataGenerator;

public sealed class DataFacade
{
    private IFixture Fixture { get; }               

        public DataFacade(IFixture fixture)
    {
        Fixture = fixture;
    }

    public DataFacade(
        string locale = "ru",
        string? scope = null,
        int recursionDepth = 3,
        int? seed = null,
        Action<Faker>? configureFaker = null)
    {
        var scopeHash = string.IsNullOrEmpty(scope) ? 0 : CryptoHelper.GetHash(scope);
        var usedSeed = (seed ?? FixtureFactory.DefaultSeed) ^ scopeHash;

        Fixture = FixtureFactory.Create(locale, usedSeed, recursionDepth, configureFaker);
    }

    public T Create<T>()
    {
        return Fixture.Create<T>();
    }

    public ICustomizationComposer<T> Build<T>()
    {
        return Fixture.Build<T>();
    }

    public T Mutate<T>(
        T source,
        int minChanges = 1,
        int maxChanges = 3,
        IEnumerable<string>? excludeProperties = null) where T : class
    {
        return DataMutator.Mutate(source, Fixture, minChanges, maxChanges, excludeProperties);
    }
}
