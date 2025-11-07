using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class StringGeneratorTests
{
    [Fact(DisplayName = "StringGenerator should generate valid values for all naming rules")]
    public void Create_AllSmartStringPatterns()
    {
        var generated = new DataFacade().Create<StringTestModel>();

        generated.EmailAddress.Should().Contain("@");

        generated.Phone.Should().MatchRegex(@"\d");
        generated.MobilePhone.Should().MatchRegex(@"\d");

        generated.FullName.Should().Contain(" ");
        generated.Name.Should().Contain(" ");
        generated.Fio.Should().Contain(" ");

        generated.FirstName.Should().NotBeNullOrWhiteSpace();
        generated.LastName.Should().NotBeNullOrWhiteSpace();
        generated.Surname.Should().NotBeNullOrWhiteSpace();

        generated.Username.Should().NotContain(" ");
        generated.Login.Should().NotContain(" ");

        generated.Password.Any(char.IsDigit).Should().BeTrue();
        generated.PwdHash.Any(char.IsDigit).Should().BeTrue();

        Uri.TryCreate(generated.WebsiteUrl, UriKind.Absolute, out _).Should().BeTrue();
        Uri.TryCreate(generated.ProfileLink, UriKind.Absolute, out _).Should().BeTrue();

        Uri.TryCreate(generated.Avatar, UriKind.Absolute, out _).Should().BeTrue();

        generated.Country.Should().NotBeNullOrWhiteSpace();
        generated.City.Should().NotBeNullOrWhiteSpace();
        generated.StreetAddress.Should().NotBeNullOrWhiteSpace();

        generated.Postcode.Should().NotBeNullOrWhiteSpace();
        generated.PostalCode.Should().NotBeNullOrWhiteSpace();
        generated.ZipCode.Should().NotBeNullOrWhiteSpace();

        generated.Title.Should().NotBeNullOrWhiteSpace();
        generated.SubjectTitle.Should().NotBeNullOrWhiteSpace();

        generated.Description.Should().NotBeNullOrWhiteSpace();
        generated.MessageText.Should().NotBeNullOrWhiteSpace();
        generated.ContentString.Should().NotBeNullOrWhiteSpace();

        generated.Company.Should().NotBeNullOrWhiteSpace();

        generated.Iban.Should().MatchRegex(@"[A-Z]{2}\w+");
        generated.AccountNumber.Should().MatchRegex(@"[A-Z]{2}\w+");

        generated.Currency.Should().MatchRegex(@"^[A-Z]{3}$");

        generated.RandomString.Should().NotBeNullOrWhiteSpace();
        generated.RandomString.Length.Should().BeInRange(8, 16);
    }
}
