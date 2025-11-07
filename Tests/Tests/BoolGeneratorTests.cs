using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class BoolGeneratorTests
{
    private const int SampleCount = 50;
    private const int MajorityThreshold = SampleCount / 2;

    [Fact(DisplayName = "BoolGenerator should generate mostly true for Is*/Has*/Enabled/Active names")]
    public void Create_MostlyPositiveFlags()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<BoolTestModel>())
            .ToList();

        generated.Count(item => item.IsActive).Should().BeGreaterThan(MajorityThreshold);
        generated.Count(item => item.HasAccess).Should().BeGreaterThan(MajorityThreshold);
        generated.Count(item => item.FeatureEnabled).Should().BeGreaterThan(MajorityThreshold);
    }

    [Fact(DisplayName = "BoolGenerator should generate mostly false for Deleted/Disabled/Blocked names")]
    public void Create_MostlyNegativeFlags()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<BoolTestModel>())
            .ToList();

        generated.Count(item => item.IsDeleted).Should().BeLessThan(MajorityThreshold);
        generated.Count(item => item.IsDisabled).Should().BeLessThan(MajorityThreshold);
        generated.Count(item => item.IsBlocked).Should().BeLessThan(MajorityThreshold);
    }

    [Fact(DisplayName = "BoolGenerator should generate both true and false for flags without naming meaning")]
    public void Create_RandomFlag_ShouldContainTrueAndFalse()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<BoolTestModel>().RandomFlag)
            .ToList();

        generated.Should().Contain(true);
        generated.Should().Contain(false);
    }
}
