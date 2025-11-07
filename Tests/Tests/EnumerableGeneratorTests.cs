using DataGenerator;

using FluentAssertions;

using Tests.TestModels;
using Tests.TestModels.Common.Enums;

using Xunit;

namespace Tests.Tests;

public sealed class EnumerableGeneratorTests
{
    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<int>")]
    public void Create_IntEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.IntEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.IntEnumerable.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<double>")]
    public void Create_DoubleEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.DoubleEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.DoubleEnumerable.Should().OnlyContain(item => !double.IsNaN(item) && !double.IsInfinity(item));
        generated.DoubleEnumerable.Should().Contain(item => Math.Abs(item) > 0.000001);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<bool>")]
    public void Create_BoolEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.BoolEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.BoolEnumerable.Should().Contain(item => item).And.Contain(item => !item);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<char>")]
    public void Create_CharEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.CharEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.CharEnumerable.Should().OnlyContain(item => char.IsLetter(item) || char.IsDigit(item) || char.IsSymbol(item) || char.IsPunctuation(item));
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<Guid>")]
    public void Create_GuidEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.GuidEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.GuidEnumerable.Should().OnlyContain(item => item != Guid.Empty);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<Uri>")]
    public void Create_UriEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.UriEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.UriEnumerable.Should().OnlyContain(item => item != null);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<DateTime>")]
    public void Create_DateEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.DateEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.DateEnumerable.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<DateOnly>")]
    public void Create_DateOnlyEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.DateOnlyEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.DateOnlyEnumerable.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<TimeOnly>")]
    public void Create_TimeOnlyEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.TimeOnlyEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.TimeOnlyEnumerable.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<TimeSpan>")]
    public void Create_TimeSpanEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.TimeSpanEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.TimeSpanEnumerable.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<enum>")]
    public void Create_EnumEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.EnumEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.EnumEnumerable.Should().OnlyContain(item => item != Sample.Unknown);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<string>")]
    public void Create_StringEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.StringEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.StringEnumerable.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IEnumerable<SimpleItem> with valid content")]
    public void Create_ItemEnumerable()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.ItemEnumerable.Should().NotBeNull().And.NotBeEmpty();
        generated.ItemEnumerable.Should().OnlyContain(item => item != null && !string.IsNullOrWhiteSpace(item.Name));
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty ICollection<string>")]
    public void Create_StringCollection()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.StringCollection.Should().NotBeNull().And.NotBeEmpty();
        generated.StringCollection.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IList<Guid>")]
    public void Create_GuidList()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.GuidList.Should().NotBeNull().And.NotBeEmpty();
        generated.GuidList.Should().OnlyContain(item => item != Guid.Empty);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IReadOnlyCollection<bool>")]
    public void Create_BoolReadOnlyCollection()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.BoolReadOnlyCollection.Should().NotBeNull().And.NotBeEmpty();
        generated.BoolReadOnlyCollection.Should().OnlyContain(item => item || !item);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty IReadOnlyList<enum>")]
    public void Create_EnumReadOnlyList()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.EnumReadOnlyList.Should().NotBeNull().And.NotBeEmpty();
        generated.EnumReadOnlyList.Should().OnlyContain(item => item != Sample.Unknown);
    }

    [Fact(DisplayName = "EnumerableGenerator should generate not empty List<SimpleItem> with valid values")]
    public void Create_ItemList()
    {
        var generated = new DataFacade().Create<EnumerableTestModel>();

        generated.ItemList.Should().NotBeNull().And.NotBeEmpty();
        generated.ItemList.Should().OnlyContain(item => item != null && !string.IsNullOrWhiteSpace(item.Name));
    }
}