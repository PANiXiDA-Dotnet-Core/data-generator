using Tests.TestModels.Common;
using Tests.TestModels.Common.Enums;

namespace Tests.TestModels;

public sealed class DictionaryTestModel
{
    public required Dictionary<int, string> IntStringDictionary { get; set; } = [];
    public required Dictionary<double, string> DoubleStringDictionary { get; set; } = [];
    public required Dictionary<string, Guid> StringGuidDictionary { get; set; } = [];
    public required Dictionary<char, bool> CharBoolDictionary { get; set; } = [];
    public required Dictionary<bool, double> BoolDoubleDictionary { get; set; } = [];
    public required Dictionary<Uri, int> UriIntDictionary { get; set; } = [];
    public required Dictionary<DateTime, string> DateStringDictionary { get; set; } = [];
    public required Dictionary<DateOnly, string> DateOnlyStringDictionary { get; set; } = [];
    public required Dictionary<TimeOnly, string> TimeOnlyStringDictionary { get; set; } = [];
    public required Dictionary<TimeSpan, string> TimeSpanStringDictionary { get; set; } = [];
    public required Dictionary<Sample, char> EnumCharDictionary { get; set; } = [];
    public required Dictionary<string, SimpleItem> StringComplexDictionary { get; set; } = [];

    public required IDictionary<Guid, int> GuidIntDictionary { get; set; } = new Dictionary<Guid, int>();
    public required IReadOnlyDictionary<string, bool> StringBoolDictionary { get; set; } = new Dictionary<string, bool>();
}
