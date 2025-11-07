using System.Reflection;

using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Implementations;
using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Core;

internal sealed class DataGenerator : ISpecimenBuilder
{
    private const float NullProbability = 0.2f;

    private readonly IReadOnlyList<ITypeDataGenerator> _generators;
    private readonly Faker _faker;
    private readonly NullabilityInfoContext _nullabilityContext;

    public DataGenerator(string locale, int seed, Action<Faker>? configureFaker)
    {
        _faker = new Faker(locale)
        {
            DateTimeReference = DateTime.UnixEpoch.AddSeconds(seed)
        };
        configureFaker?.Invoke(_faker);

        _generators =
        [
            new StringGenerator(_faker),
            new NumericGenerator(_faker),
            new BoolGenerator(_faker),
            new EnumGenerator(_faker),
            new DateTimeGenerator(_faker),
            new GuidGenerator(_faker),
            new UriGenerator(_faker),
            new CharGenerator(_faker),
            new ArrayGenerator(_faker),
            new EnumerableGenerator(_faker),
            new DictionaryGenerator(_faker),
        ];

        _nullabilityContext = new();
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is PropertyInfo propertyInfo)
        {
            return CreateForProperty(propertyInfo, context);
        }
        if (request is Type type)
        {
            return CreateForType(type, context);
        }

        return new NoSpecimen();
    }

    private object CreateForProperty(PropertyInfo propertyInfo, ISpecimenContext context)
    {
        var type = propertyInfo.PropertyType;
        var name = propertyInfo.Name;

        if (ShouldGenerateNull(propertyInfo, type))
        {
            return null!;
        }
        if (TryGenerateKnown(type, name, context, out var value))
        {
            return value!;
        }

        return context.Resolve(type);
    }

    private object CreateForType(Type type, ISpecimenContext context)
    {
        if (TryGenerateKnown(type, name: null, context, out var value))
        {
            return value!;
        }

        return new NoSpecimen();
    }

    private bool ShouldGenerateNull(PropertyInfo property, Type type)
    {
        if (TryGenerateNullForNullableValueType(type))
        {
            return true;
        }
        if (TryGenerateNullForNullableReferenceType(property, type))
        {
            return true;
        }

        return false;
    }

    private bool TryGenerateNullForNullableValueType(Type type)
    {
        var underlying = Nullable.GetUnderlyingType(type);
        if (underlying != null)
        {
            return _faker.Random.Bool(NullProbability);
        }

        return false;
    }

    private bool TryGenerateNullForNullableReferenceType(PropertyInfo property, Type type)
    {
        if (type.IsValueType)
        {
            return false;
        }

        var info = _nullabilityContext.Create(property);
        if (info.ReadState == NullabilityState.Nullable)
        {
            return _faker.Random.Bool(NullProbability);
        }

        return false;
    }

    private bool TryGenerateKnown(Type type, string? name, ISpecimenContext context, out object? result)
    {
        foreach (var generator in _generators)
        {
            if (generator.TryGenerate(type, name, context, out result))
            {
                return true;
            }
        }

        result = null;

        return false;
    }
}
