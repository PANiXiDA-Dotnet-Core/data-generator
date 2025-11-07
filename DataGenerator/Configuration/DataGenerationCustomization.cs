using AutoFixture;

using Bogus;

namespace DataGenerator.Configuration;

internal sealed class DataGenerationCustomization(string locale, int seed, Action<Faker>? configureFaker, int recursionDepth) : ICustomization
{
    public void Customize(IFixture fixture)
    {
        foreach (var behavior in fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList())
        {
            fixture.Behaviors.Remove(behavior);
        }
        fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth));
        fixture.Customizations.Insert(0, new Core.DataGenerator(locale, seed, configureFaker));
    }
}
