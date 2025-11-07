using DataGenerator;

using FluentAssertions;

using Tests.TestModels;
using Tests.TestModels.Common.Enums;

using Xunit;

namespace Tests.Tests;

public sealed class EnumGeneratorTests
{
    [Fact(DisplayName = "EnumGenerator should never generate zero-value for enums starting with Unknown/None")]
    public void Create_ShouldSkipZeroValue_WhenEnumStartsWithZero()
    {
        var generated = new DataFacade().Create<EnumTestModel>();

        generated.Basic.Should().NotBe(Basic.Unknown);
    }

    [Fact(DisplayName = "EnumGenerator should generate valid values for enums without zero")]
    public void Create_ShouldGenerate_WhenEnumHasNoZero()
    {
        var generated = new DataFacade().Create<EnumTestModel>();

        generated.NoZero.Should().BeOneOf(NoZero.A, NoZero.B, NoZero.C);
    }

    [Fact(DisplayName = "EnumGenerator should generate any value except zero entry when zero exists but is not 'Unknown' conceptually")]
    public void Create_ShouldSkipZero_WhenZeroExistsButNotMeaningful()
    {
        var generated = new DataFacade().Create<EnumTestModel>();

        generated.Weird.Should().BeOneOf(Weird.Ten, Weird.Minus);
    }

    [Fact(DisplayName = "EnumGenerator should produce different values statistically over multiple generations")]
    public void Create_ShouldProduceDiversity()
    {
        var facade = new DataFacade();

        var results = Enumerable.Range(0, 50)
            .Select(_ => facade.Create<EnumTestModel>().Basic)
            .Distinct()
            .ToList();

        results.Count.Should().BeGreaterThan(1);
    }
}
