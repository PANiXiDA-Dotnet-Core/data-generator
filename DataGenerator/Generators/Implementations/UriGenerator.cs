using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class UriGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(Uri))
        {
            value = new Uri(faker.Internet.Url());
            return true;
        }

        value = null;

        return false;
    }
}
