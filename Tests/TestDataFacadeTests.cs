using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests;

public sealed class TestDataFacadeTests
{
    [Fact(DisplayName = "Same seed should produce identical results")]
    public void Create_WithSameSeed_ProducesSameData()
    {
        var a = new DataFacade(seed: 123);
        var b = new DataFacade(seed: 123);

        var user1 = a.Create<User>();
        var user2 = b.Create<User>();

        user1.Should().BeEquivalentTo(user2);
    }

    [Fact(DisplayName = "Different seeds should produce different results")]
    public void Create_WithDifferentSeed_ProducesDifferentData()
    {
        var a = new DataFacade(seed: 1);
        var b = new DataFacade(seed: 2);

        var user1 = a.Create<User>();
        var user2 = b.Create<User>();

        user1.Should().NotBeEquivalentTo(user2);
    }
}
