using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class GuidGeneratorTests
{
    [Fact(DisplayName = "GuidGenerator should generate non-empty Guid values")]
    public void Create_GeneratesNonEmptyGuids()
    {
        var generated = new DataFacade().Create<GuidTestModel>();

        generated.Id.Should().NotBe(Guid.Empty);
        generated.ExternalId.Should().NotBe(Guid.Empty);
        generated.CorrelationId.Should().NotBe(Guid.Empty);
    }

    [Fact(DisplayName = "GuidGenerator should generate different Guid values across multiple samples")]
    public void Create_GeneratesDifferentValues_Statistically()
    {
        var facade = new DataFacade();

        var results = Enumerable.Range(0, 30)
            .Select(_ => facade.Create<GuidTestModel>().Id)
            .ToList();

        results.Distinct().Count().Should().BeGreaterThan(1);
    }
}
