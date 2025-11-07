using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class BoolGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(bool))
        {
            value = name is not null ? GenerateSmartBool(name) : faker.Random.Bool();
            return true;
        }

        value = null;

        return false;
    }

    private bool GenerateSmartBool(string propertyName)
    {
        var name = propertyName.ToLowerInvariant();

        if (name.Contains("deleted") || name.Contains("disabled") || name.Contains("blocked"))
        {
            return faker.Random.Bool(0.1f);
        }
        if (name.StartsWith("is") || name.StartsWith("has") || name.Contains("enabled") || name.Contains("active"))
        {
            return faker.Random.Bool(0.7f);
        }

        return faker.Random.Bool();
    }
}
