using DataGenerator;

using FluentAssertions;

using Tests.TestModels;
using Tests.TestModels.Common.Enums;

using Xunit;

namespace Tests.Tests;

public sealed class DictionaryGeneratorTests
{
    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<int,string>")]
    public void Create_IntStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.IntStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.IntStringDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.IntStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<double,string>")]
    public void Create_DoubleStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.DoubleStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.DoubleStringDictionary.Keys.Should().OnlyContain(item => !double.IsNaN(item) && !double.IsInfinity(item));
        generated.DoubleStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<string,Guid>")]
    public void Create_StringGuidDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.StringGuidDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.StringGuidDictionary.Keys.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
        generated.StringGuidDictionary.Values.Should().OnlyContain(item => item != Guid.Empty);
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<char,bool>")]
    public void Create_CharBoolDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.CharBoolDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.CharBoolDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.CharBoolDictionary.Values.Should().OnlyContain(item => item || !item);
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<bool,double>")]
    public void Create_BoolDoubleDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.BoolDoubleDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.BoolDoubleDictionary.Keys.Should().Contain(true).And.Contain(false);
        generated.BoolDoubleDictionary.Values.Should().OnlyContain(item => !double.IsNaN(item) && !double.IsInfinity(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<Uri,int>")]
    public void Create_UriIntDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.UriIntDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.UriIntDictionary.Keys.Should().OnlyContain(item => item != null);
        generated.UriIntDictionary.Values.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<DateTime,string>")]
    public void Create_DateStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.DateStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.DateStringDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.DateStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<DateOnly,string>")]
    public void Create_DateOnlyStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.DateOnlyStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.DateOnlyStringDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.DateOnlyStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<TimeOnly,string>")]
    public void Create_TimeOnlyStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.TimeOnlyStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.TimeOnlyStringDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.TimeOnlyStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<TimeSpan,string>")]
    public void Create_TimeSpanStringDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.TimeSpanStringDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.TimeSpanStringDictionary.Keys.Should().OnlyContain(item => item != default);
        generated.TimeSpanStringDictionary.Values.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<enum,char>")]
    public void Create_EnumCharDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.EnumCharDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.EnumCharDictionary.Keys.Should().OnlyContain(item => item != Sample.Unknown);
        generated.EnumCharDictionary.Values.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty Dictionary<string,SimpleItem> with valid values")]
    public void Create_StringComplexDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.StringComplexDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.StringComplexDictionary.Keys.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
        generated.StringComplexDictionary.Values.Should().OnlyContain(item => item != null && !string.IsNullOrWhiteSpace(item.Name));
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty IDictionary<Guid,int>")]
    public void Create_GuidIntDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.GuidIntDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.GuidIntDictionary.Keys.Should().OnlyContain(item => item != Guid.Empty);
        generated.GuidIntDictionary.Values.Should().OnlyContain(item => item != default);
    }

    [Fact(DisplayName = "DictionaryGenerator should generate not empty IReadOnlyDictionary<string,bool>")]
    public void Create_StringBoolDictionary()
    {
        var generated = new DataFacade().Create<DictionaryTestModel>();

        generated.StringBoolDictionary.Should().NotBeNull().And.NotBeEmpty();
        generated.StringBoolDictionary.Keys.Should().OnlyContain(item => !string.IsNullOrWhiteSpace(item));
        generated.StringBoolDictionary.Values.Should().Contain(item => item).And.Contain(item => !item);
    }
}