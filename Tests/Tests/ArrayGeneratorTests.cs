using DataGenerator;

using FluentAssertions;

using Tests.TestModels;
using Tests.TestModels.Common.Enums;

using Xunit;

namespace Tests.Tests;

public sealed class ArrayGeneratorTests
{
    [Fact(DisplayName = "ArrayGenerator should generate not empty int[]")]
    public void Create_IntArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Ints.Should().NotBeNull();
        generated.Ints.Should().NotBeEmpty();
        generated.Ints.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty double[]")]
    public void Create_DoubleArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Doubles.Should().NotBeNull();
        generated.Doubles.Should().NotBeEmpty();
        generated.Doubles.Should().OnlyContain(item => !double.IsNaN(item) && !double.IsInfinity(item));
        generated.Doubles.Should().Contain(item => Math.Abs(item) > 0.000001);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty bool[]")]
    public void Create_BoolArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Bools.Should().NotBeNull();
        generated.Bools.Should().NotBeEmpty();

        generated.Bools.Should().Contain(item => item).And.Contain(item => !item);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty char[]")]
    public void Create_CharArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Chars.Should().NotBeNull();
        generated.Chars.Should().NotBeEmpty();

        generated.Chars.Should().OnlyContain(item =>
            char.IsLetter(item) || char.IsDigit(item) || char.IsSymbol(item) || char.IsPunctuation(item));
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty Guid[] with valid values")]
    public void Create_GuidArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Guids.Should().NotBeNull();
        generated.Guids.Should().NotBeEmpty();
        generated.Guids.Should().OnlyContain(item => item != Guid.Empty);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty Uri[]")]
    public void Create_UriArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Uris.Should().NotBeNull();
        generated.Uris.Should().NotBeEmpty();
        generated.Uris.Should().OnlyContain(item => item != null);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty DateTime[]")]
    public void Create_DateArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Dates.Should().NotBeNull();
        generated.Dates.Should().NotBeEmpty();
        generated.Dates.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty DateOnly[]")]
    public void Create_DateOnlyArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.DateOnlys.Should().NotBeNull();
        generated.DateOnlys.Should().NotBeEmpty();
        generated.DateOnlys.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty TimeOnly[]")]
    public void Create_TimeOnlyArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.TimeOnlys.Should().NotBeNull();
        generated.TimeOnlys.Should().NotBeEmpty();
        generated.TimeOnlys.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty TimeSpan[]")]
    public void Create_TimeSpanArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.TimeSpans.Should().NotBeNull();
        generated.TimeSpans.Should().NotBeEmpty();
        generated.TimeSpans.Should().OnlyContain(item => item != default);
    }


    [Fact(DisplayName = "ArrayGenerator should generate not empty enum[] without Unknown")]
    public void Create_EnumArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Enums.Should().NotBeNull();
        generated.Enums.Should().NotBeEmpty();
        generated.Enums.Should().OnlyContain(item => item != Sample.Unknown);
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty string[] with non-empty values")]
    public void Create_StringArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Strings.Should().NotBeNull();
        generated.Strings.Should().NotBeEmpty();
        generated.Strings.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "ArrayGenerator should generate not empty SimpleItem[]")]
    public void Create_ObjectArray()
    {
        var generated = new DataFacade().Create<ArrayTestModel>();

        generated.Items.Should().NotBeNull();
        generated.Items.Should().NotBeEmpty();
        generated.Items.Should().OnlyContain(item => item != null && !string.IsNullOrWhiteSpace(item.Name));
    }
}
