using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests;

public sealed class DataGeneratorTests
{
    [Fact(DisplayName = "Create<T> should populate all supported property types")]
    public void Create_PopulatesAllTypes()
    {
        var facade = new DataFacade();

        var generated = facade.Create<SampleModel>();

        generated.ExternalId.Should().NotBe(Guid.Empty);

        generated.Login.Should().NotBeNullOrWhiteSpace();
        generated.Password.Should().NotBeNullOrWhiteSpace();

        generated.Email.Should().Contain("@");
        generated.DisplayName.Should().NotBeNullOrWhiteSpace();

        generated.Age.Should().BeInRange(18, 70);

        generated.CreatedAt.Should().BeBefore(DateTime.UtcNow);
        generated.UpdatedAt.Should().BeBefore(DateTime.UtcNow);

        generated.AvatarUrl.Should().NotBeNull();

        generated.Status.Should().NotBe(Sample.Unknown);

        generated.Scores.Should().NotBeNull();
        generated.Scores.Length.Should().BeGreaterThan(0);

        generated.Tags.Should().NotBeNull();
        generated.Tags.Should().NotBeEmpty();

        generated.Metadata.Should().NotBeNull();
        generated.Metadata.Should().NotBeEmpty();
    }
}
