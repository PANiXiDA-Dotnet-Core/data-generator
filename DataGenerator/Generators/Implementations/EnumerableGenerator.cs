using System.Collections;

using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class EnumerableGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        value = null;

        if (type.IsArray)
        {
            return false;
        }
        if (DictionaryGenerator.TryGetDictionaryTypes(type, out var _, out var _))
        {
            return false;
        }
        if (!TryGetEnumerableElementType(type, out var elemType))
        {
            return false;
        }

        var count = faker.Random.Int(2, 6);
        var listType = typeof(List<>).MakeGenericType(elemType!);
        var list = (IList)Activator.CreateInstance(listType)!;

        for (int i = 0; i < count; i++)
        {
            var item = context.Resolve(elemType!);
            if (item is null && !elemType!.IsValueType) continue;
            list.Add(item);
        }
        value = list;

        return true;
    }

    private static bool TryGetEnumerableElementType(Type type, out Type? elementType)
    {
        Type[] SupportedEnumerableDefinitions =
        {
            typeof(IEnumerable<>),
            typeof(ICollection<>),
            typeof(IList<>),
            typeof(IReadOnlyCollection<>),
            typeof(IReadOnlyList<>),
            typeof(List<>)
        };

        elementType = null;

        if (type.IsGenericType && SupportedEnumerableDefinitions.Contains(type.GetGenericTypeDefinition()))
        {
            elementType = type.GetGenericArguments()[0];
            return true;
        }

        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsGenericType &&
                SupportedEnumerableDefinitions.Contains(@interface.GetGenericTypeDefinition()))
            {
                elementType = @interface.GetGenericArguments()[0];
                return true;
            }
        }

        return false;
    }
}
