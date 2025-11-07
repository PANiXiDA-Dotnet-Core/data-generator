using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class ArrayGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        value = null;

        if (!TryGetArrayElementType(type, out var elemType))
        {
            return false;
        }
        if (context.Resolve(elemType!) is OmitSpecimen or NoSpecimen)
        {
            value = Array.CreateInstance(elemType!, 0);
            return true;
        }

        var count = faker.Random.Int(2, 6);
        var array = Array.CreateInstance(elemType!, count);

        for (int i = 0; i < count; i++)
        {
            var item = context.Resolve(elemType!);
            if (item is null && !elemType!.IsValueType)
            {
                continue;
            }
            array.SetValue(item, i);
        }
        value = array;

        return true;
    }

    private static bool TryGetArrayElementType(Type type, out Type? elementType)
    {
        elementType = type.IsArray ? type.GetElementType() : null;
        return elementType != null;
    }
}
