using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class NumericGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(int))
        {
            value = name is not null ? GenerateSmartInt(name) : faker.Random.Int(0, 10_000);
            return true;
        }
        if (type == typeof(uint))
        {
            value = name is not null ? GenerateSmartInt(name) : faker.Random.Int(0, 10_000);
            return true;
        }
        if (type == typeof(long))
        {
            value = faker.Random.Long(0, long.MaxValue);
            return true;
        }
        if (type == typeof(ulong))
        {
            value = (ulong)faker.Random.Long(0, long.MaxValue);
            return true;
        }
        if (type == typeof(short))
        {
            value = (short)faker.Random.Int(short.MinValue, short.MaxValue);
            return true;
        }
        if (type == typeof(ushort))
        {
            value = (ushort)faker.Random.Int(0, ushort.MaxValue);
            return true;
        }
        if (type == typeof(byte))
        {
            value = faker.Random.Byte();
            return true;
        }
        if (type == typeof(sbyte))
        {
            value = (sbyte)faker.Random.Int(sbyte.MinValue, sbyte.MaxValue);
            return true;
        }
        if (type == typeof(double))
        {
            value = Math.Round(faker.Random.Double(0, 100_000), 4);
            return true;
        }
        if (type == typeof(float))
        {
            value = (float)Math.Round(faker.Random.Double(0, 100_000), 4);
            return true;
        }
        if (type == typeof(decimal))
        {
            value = Math.Round(faker.Random.Decimal(0, 100_000), 2);
            return true;
        }

        value = null;

        return false;
    }

    private int GenerateSmartInt(string propertyName)
    {
        var name = propertyName.ToLowerInvariant();

        if (name == "id" || name.EndsWith("id"))
        {
            return 0;
        }
        if (name.Contains("age"))
        {
            return faker.Random.Int(18, 70);
        }
        if (name.Contains("count") || name.Contains("qty") || name.EndsWith("num"))
        {
            return faker.Random.Int(0, 1000);
        }
        if (name.Contains("price") || name.Contains("amount") || name.Contains("sum"))
        {
            return faker.Random.Int(1, 100_000);
        }
        if (name.Contains("year"))
        {
            return faker.Date.Between(new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), faker.DateTimeReference ?? DateTime.UtcNow).Year;
        }

        return faker.Random.Int(0, 10_000);
    }
}
