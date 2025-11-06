using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class CharGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(char))
        {
            var dice = faker.Random.Double();
            char generatedChar;

            if (dice < 0.7f)
            {
                generatedChar = faker.Random.Char('a', 'z');
            }
            else if (dice < 0.9f)
            {
                generatedChar = faker.Random.Char('0', '9');
            }
            else
            {
                generatedChar = faker.Random.Char('\u0021', '\u007E');
            }
            value = generatedChar;

            return true;
        }

        value = null;

        return false;
    }
}
