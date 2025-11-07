using System.Collections;
using System.Collections.Concurrent;

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
        if (ShouldReturnEmptyDictionary(keyType!, valueType!, context))
        {
            value = CreateEmptyDictionary(type, keyType!, valueType!);
            return true;
        }

        var count = faker.Random.Int(2, 5);
        var dictionaryInstance = CreateEmptyDictionary(type, keyType!, valueType!)!;

        if (dictionaryInstance is ConcurrentDictionary<object, object> concurrentDict)
        {
            FillConcurrent(concurrentDict, keyType!, valueType!, context, count);
            value = concurrentDict;
            return true;
        }
        else
        {
            var dictionary = (IDictionary)dictionaryInstance;
            FillDictionary(dictionary, keyType!, valueType!, context, count);
            value = dictionary;
        }

        return true;
    }

    public static bool TryGetDictionaryTypes(Type type, out Type? keyType, out Type? valueType)
    {
        keyType = valueType = null;

        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(Dictionary<,>) ||
                genericTypeDefinition == typeof(IDictionary<,>) ||
                genericTypeDefinition == typeof(IReadOnlyDictionary<,>) ||
                genericTypeDefinition == typeof(SortedDictionary<,>) ||
                genericTypeDefinition == typeof(ConcurrentDictionary<,>))
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
        var type = Nullable.GetUnderlyingType(keyType) ?? keyType;

        var generator = CreateScalarGenerator(type);
        if (generator is not null && generator.TryGenerate(type, name: null, context, out var value))
        {
            return value;
        }

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

    private static bool ShouldReturnEmptyDictionary(Type keyType, Type valueType, ISpecimenContext context)
    {
        var keyProbe = context.Resolve(keyType);
        if (keyProbe is OmitSpecimen or NoSpecimen)
        {
            return true;
        }

        var valProbe = context.Resolve(valueType);
        return valProbe is OmitSpecimen or NoSpecimen;
    }

    private static object CreateEmptyDictionary(Type requestType, Type keyType, Type valueType)
    {
        if (!requestType.IsInterface && !requestType.IsAbstract)
        {
            return Activator.CreateInstance(requestType)!;
        }
        if (requestType.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>))
        {
            return Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(keyType, valueType))!;
        }
        if (requestType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
        {
            return Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(keyType, valueType))!;
        }

        return Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(keyType, valueType))!;
    }

    private void FillDictionary(IDictionary dictionary, Type keyType, Type valueType, ISpecimenContext context, int count)
    {
        FillDictionaryCore(
            getCount: () => dictionary.Count,
            generateKey: () => GenerateKey(keyType, context),
            generateValue: () => context.Resolve(valueType),
            tryAdd: (key, value) =>
            {
                if (!dictionary.Contains(key))
                {
                    dictionary.Add(key, value);
                    return true;
                }
                return false;
            },
            count: count
        );
    }

    private void FillConcurrent(ConcurrentDictionary<object, object> dictionary, Type keyType, Type valueType, ISpecimenContext context, int count)
    {
        FillDictionaryCore(
            getCount: () => dictionary.Count,
            generateKey: () => GenerateKey(keyType, context),
            generateValue: () => context.Resolve(valueType),
            tryAdd: (key, value) => dictionary.TryAdd(key!, value!),
            count: count
        );
    }

    private static void FillDictionaryCore(
        Func<int> getCount,
        Func<object?> generateKey,
        Func<object?> generateValue,
        Func<object, object?, bool> tryAdd,
        int count)
    {
        int safety = 0;
        while (getCount() < count && safety < count * 4)
        {
            var key = generateKey();
            if (key is null)
            {
                safety++;
                continue;
            }

            var value = generateValue();
            tryAdd(key, value);

            safety++;
        }
    }
}
