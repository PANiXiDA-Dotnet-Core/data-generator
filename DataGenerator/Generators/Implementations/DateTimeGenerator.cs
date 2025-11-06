using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class DateTimeGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(DateTime))
        {
            value = name is not null ? GenerateSmartDate(name) : faker.Date.Recent(30);
            return true;
        }
        if (type == typeof(TimeSpan))
        {
            value = faker.Date.Timespan();
            return true;
        }
        if (type == typeof(DateOnly))
        {
            var dateTime = faker.Date.Between(new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), faker.DateTimeReference ?? DateTime.UtcNow);
            value = DateOnly.FromDateTime(dateTime);
            return true;
        }
        if (type == typeof(TimeOnly))
        {
            var dateTime = faker.Date.Between(new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc), faker.DateTimeReference ?? DateTime.UtcNow);
            value = TimeOnly.FromDateTime(dateTime);
            return true;
        }

        value = null;

        return false;
    }

    private DateTime GenerateSmartDate(string propertyName)
    {
        var name = propertyName.ToLowerInvariant();

        if (name.Contains("created"))
        {
            return faker.Date.Past(2);
        }
        if (name.Contains("updated") || name.Contains("modified"))
        {
            return faker.Date.Recent(30);
        }
        if (name.Contains("deleted"))
        {
            return faker.Date.Recent(30);
        }
        if (name.Contains("birth") || name.Contains("birthday") || name.Contains("dob"))
        {
            return faker.Date.Past(60, faker.DateTimeReference?.AddYears(-18));
        }
        if (name.Contains("registered") || name.Contains("signup"))
        {
            return faker.Date.Past(3);
        }

        return faker.Date.Recent(90);
    }
}
