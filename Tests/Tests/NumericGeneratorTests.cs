using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class NumericGeneratorTests
{
    [Fact(DisplayName = "NumericGenerator should generate correct int values based on naming rules")]
    public void Create_IntNamingPatterns()
    {
        var generated = new DataFacade().Create<NumericTestModel>();

        generated.Id.Should().Be(0);
        generated.UserId.Should().Be(0);

        generated.Age.Should().BeInRange(18, 70);

        generated.ItemCount.Should().BeInRange(0, 1000);
        generated.Qty.Should().BeInRange(0, 1000);
        generated.TotalNum.Should().BeInRange(0, 1000);

        generated.Price.Should().BeInRange(1, 100_000);
        generated.OrderAmount.Should().BeInRange(1, 100_000);
        generated.TotalSum.Should().BeInRange(1, 100_000);

        generated.ReleaseYear.Should().BeLessThanOrEqualTo(DateTime.UtcNow.Year);
    }

    [Fact(DisplayName = "NumericGenerator should generate correct numeric values for all numeric types")]
    public void Create_NumericTypes()
    {
        var generated = new DataFacade().Create<NumericTestModel>();

        generated.UIntValue.Should().BeGreaterThanOrEqualTo(0u);
        generated.LongValue.Should().BeGreaterThanOrEqualTo(0);
        generated.ULongValue.Should().BeGreaterThanOrEqualTo(0ul);
        generated.ShortValue.Should().BeInRange(short.MinValue, short.MaxValue);
        generated.UShortValue.Should().BeInRange(ushort.MinValue, ushort.MaxValue);
        generated.ByteValue.Should().BeInRange(byte.MinValue, byte.MaxValue);
        generated.SByteValue.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

        generated.DoubleValue.Should().BeGreaterThan(0).And.BeLessThanOrEqualTo(100_000);
        generated.FloatValue.Should().BeGreaterThan(0).And.BeLessThanOrEqualTo(100_000);
        generated.DecimalValue.Should().BeGreaterThan(0).And.BeLessThanOrEqualTo(100_000);
    }

    [Fact(DisplayName = "NumericGenerator should produce statistically diverse values")]
    public void Create_ShouldProduceDiversity()
    {
        var facade = new DataFacade();

        var values = Enumerable.Range(0, 50)
            .Select(_ => facade.Create<NumericTestModel>().DoubleValue)
            .ToList();

        values.Distinct().Count().Should().BeGreaterThan(1);
    }
}
