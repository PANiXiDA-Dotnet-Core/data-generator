using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class DateTimeGeneratorTests
{
    private static readonly DateTime Now = DateTime.UtcNow;

    [Fact(DisplayName = "DateTimeGenerator should generate dates based on naming conventions")]
    public void Create_GeneratesExpectedDateTimeValues()
    {
        var facade = new DataFacade();
        var generated = facade.Create<DateTimeTestModel>();

        generated.CreatedAt.Should().BeBefore(Now);
        generated.UpdatedAt.Should().BeBefore(Now);
        generated.ModifiedAt.Should().BeBefore(Now);

        if (generated.DeletedAt is not null)
        {
            generated.DeletedAt.Value.Should().BeBefore(Now);
        }

        (Now.Year - generated.Birthday.Year).Should().BeInRange(18, 120);
        (Now.Year - generated.Dob.Year).Should().BeInRange(18, 120);

        generated.RegisteredAt.Should().BeBefore(Now);
        generated.SignupDate.Should().BeBefore(Now);
    }

    [Fact(DisplayName = "DateTimeGenerator should generate TimeSpan values")]
    public void Create_GeneratesTimeSpan()
    {
        var generated = new DataFacade().Create<DateTimeTestModel>();

        generated.Duration.Should().NotBe(default);
        generated.Duration.TotalSeconds.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "DateTimeGenerator should generate DateOnly values")]
    public void Create_GeneratesDateOnly()
    {
        var generated = new DataFacade().Create<DateTimeTestModel>();
        generated.BirthDateOnly.Should().NotBe(default);
    }

    [Fact(DisplayName = "DateTimeGenerator should generate TimeOnly values")]
    public void Create_GeneratesTimeOnly()
    {
        var generated = new DataFacade().Create<DateTimeTestModel>();

        generated.WakeUpTime.Hour.Should().BeInRange(0, 23);
        generated.WakeUpTime.Minute.Should().BeInRange(0, 59);
    }
}
