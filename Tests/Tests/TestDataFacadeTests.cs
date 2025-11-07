using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class TestDataFacadeTests
{
    [Fact(DisplayName = "Same seed should produce identical results")]
    public void Create_WithSameSeed_ProducesSameData()
    {
        var a = new DataFacade(seed: 123);
        var b = new DataFacade(seed: 123);

        var generated1 = a.Create<SampleModel>();
        var generated2 = b.Create<SampleModel>();

        generated1.Should().BeEquivalentTo(generated2);
    }

    [Fact(DisplayName = "Different seeds should produce different results")]
    public void Create_WithDifferentSeed_ProducesDifferentData()
    {
        var a = new DataFacade(seed: 1);
        var b = new DataFacade(seed: 2);

        var generated1 = a.Create<SampleModel>();
        var generated2 = b.Create<SampleModel>();

        generated1.Should().NotBeEquivalentTo(generated2);
    }
}
