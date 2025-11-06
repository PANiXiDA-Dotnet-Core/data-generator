using System.Reflection;

using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Implementations;
using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Core;

internal sealed class DataGenerator : ISpecimenBuilder
{
    private readonly IReadOnlyList<ITypeDataGenerator> _generators;
    private readonly Faker _faker;

    public DataGenerator(string locale, Action<Faker>? configureFaker)
    {
        _faker = new Faker(locale)
        {
            DateTimeReference = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc)
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

    private bool TryGenerateKnown(Type type, string? name, ISpecimenContext context, out object? result)
    {
        var underlying = Nullable.GetUnderlyingType(type);
        if (underlying != null)
        {
            if (_faker.Random.Bool(0.2f))
            {
                result = null;
                return true;
            }

            type = underlying;
        }

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
