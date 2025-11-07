using System.Collections;

using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class DictionaryGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        value = null;

        if (!TryGetDictionaryTypes(type, out var keyType, out var valueType))
        {
            return false;
        }

        var count = faker.Random.Int(2, 5);
        var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType!, valueType!);
        var dictionary = (IDictionary)Activator.CreateInstance(dictionaryType)!;

        int safety = 0;
        while (dictionary.Count < count && safety < count * 4)
        {
            var key = GenerateKey(keyType!, context);
            if (key is null)
            {
                safety++;
                continue;
            }

            var val = context.Resolve(valueType!);

            if (!dictionary.Contains(key))
            {
                dictionary.Add(key, val);
            }

            safety++;
        }
        value = dictionary;

        return true;
    }

    public static bool TryGetDictionaryTypes(Type type, out Type? keyType, out Type? valueType)
    {
        keyType = valueType = null;

        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(IDictionary<,>) ||
                genericTypeDefinition == typeof(Dictionary<,>) ||
                genericTypeDefinition == typeof(IReadOnlyDictionary<,>))
            {
                var args = type.GetGenericArguments();
                keyType = args[0];
                valueType = args[1];
                return true;
            }
        }

        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsGenericType)
            {
                var genericTypeDefinition = @interface.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IDictionary<,>) || genericTypeDefinition == typeof(IReadOnlyDictionary<,>))
                {
                    var args = @interface.GetGenericArguments();
                    keyType = args[0];
                    valueType = args[1];
                    return true;
                }
            }
        }

        return false;
    }

    private object? GenerateKey(Type keyType, ISpecimenContext context)
    {
        var t = Nullable.GetUnderlyingType(keyType) ?? keyType;

        var gen = CreateScalarGenerator(t);
        if (gen is not null && gen.TryGenerate(t, name: null, context, out var value))
            return value;

        return context.Resolve(keyType);
    }

    private ITypeDataGenerator? CreateScalarGenerator(Type type)
    {
        if (type.IsEnum)
        {
            return new EnumGenerator(faker);
        }
        if (type == typeof(string))
        {
            return new StringGenerator(faker);
        }
        if (type == typeof(Guid))
        {
            return new GuidGenerator(faker);
        }
        if (type == typeof(bool))
        {
            return new BoolGenerator(faker);
        }
        if (type == typeof(char))
        {
            return new CharGenerator(faker);
        }
        if (type == typeof(Uri))
        {
            return new UriGenerator(faker);
        }
        if (type == typeof(int) || type == typeof(long) || type == typeof(short) ||
            type == typeof(byte) || type == typeof(uint) || type == typeof(ulong) ||
            type == typeof(ushort) || type == typeof(float) || type == typeof(double) ||
            type == typeof(decimal) || type == typeof(sbyte))
        {
            return new NumericGenerator(faker);
        }
        if (type == typeof(DateTime) || type == typeof(DateOnly) || type == typeof(TimeOnly) || type == typeof(TimeSpan))
        {
            return new DateTimeGenerator(faker);
        }

        return null;
    }
}
