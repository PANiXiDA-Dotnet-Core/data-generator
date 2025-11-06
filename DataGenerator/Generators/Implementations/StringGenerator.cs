using AutoFixture.Kernel;

using Bogus;

using DataGenerator.Generators.Interfaces;

namespace DataGenerator.Generators.Implementations;

internal sealed class StringGenerator(Faker faker) : ITypeDataGenerator
{
    public bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value)
    {
        if (type == typeof(string))
        {
            value = name is not null ? GenerateSmartString(name) : faker.Random.AlphaNumeric(12);
            return true;
        }

        value = null;

        return false;
    }

    private string GenerateSmartString(string propertyName)
    {
        var name = propertyName.ToLowerInvariant();

        if (name.Contains("email"))
        {
            return faker.Internet.Email();
        }
        if (name.Contains("phone") || name.Contains("mobile"))
        {
            return faker.Phone.PhoneNumber();
        }
        if (name.Contains("fullname") || name == "name" || name.Contains("fio"))
        {
            return faker.Name.FullName();
        }
        if (name.Contains("firstname"))
        {
            return faker.Name.FirstName();
        }
        if (name.Contains("lastname") || name.Contains("surname"))
        {
            return faker.Name.LastName();
        }
        if (name.Contains("username") || name == "login")
        {
            return faker.Internet.UserName();
        }
        if (name.Contains("password") || name.Contains("pwd"))
        {
            var password = string.Empty;
            while (!password.Any(char.IsDigit))
            {
                password = faker.Internet.Password();
            }
            return password;
        }
        if (name.Contains("url") || name.Contains("link"))
        {
            return faker.Internet.Url();
        }
        if (name.Contains("avatar"))
        {
            return faker.Internet.Avatar();
        }
        if (name.Contains("country"))
        {
            return faker.Address.Country();
        }
        if (name.Contains("city"))
        {
            return faker.Address.City();
        }
        if (name.Contains("street"))
        {
            return faker.Address.StreetAddress();
        }
        if (name.Contains("postcode") || name.Contains("postal") || name.Contains("zip"))
        {
            return faker.Address.ZipCode();
        }
        if (name.Contains("title") || name.Contains("subject"))
        {
            return faker.Lorem.Sentence(3);
        }
        if (name.Contains("desc") || name.Contains("message") || name.Contains("text") || name.Contains("content"))
        {
            return faker.Lorem.Sentences(faker.Random.Int(1, 3));
        }
        if (name.Contains("company"))
        {
            return faker.Company.CompanyName();
        }
        if (name.Contains("iban") || name.Contains("account"))
        {
            return faker.Finance.Iban();
        }
        if (name.Contains("currency"))
        {
            return faker.Finance.Currency().Code;
        }

        return faker.Random.AlphaNumeric(faker.Random.Int(8, 16));
    }
}

