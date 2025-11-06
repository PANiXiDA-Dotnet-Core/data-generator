using AutoFixture;

using Bogus;

namespace DataGenerator.Configuration;

internal sealed class DataGenerationCustomization(string locale = "ru", Action<Faker>? configureFaker = null, int recursionDepth = 2) : ICustomization
{
    public void Customize(IFixture fixture)
    {
        foreach (var behavior in fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList())
        {
            fixture.Behaviors.Remove(behavior);
        }
        fixture.Behaviors.Add(new OmitOnRecursionBehavior(recursionDepth));
        fixture.Customizations.Insert(0, new Core.DataGenerator(locale, configureFaker));
    }
}
