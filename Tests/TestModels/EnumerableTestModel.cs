using Tests.TestModels.Common;
using Tests.TestModels.Common.Enums;

namespace Tests.TestModels;

public sealed class EnumerableTestModel
{
    public required IEnumerable<int> IntEnumerable { get; set; } = [];
    public required IEnumerable<double> DoubleEnumerable { get; set; } = [];
    public required IEnumerable<bool> BoolEnumerable { get; set; } = [];
    public required IEnumerable<char> CharEnumerable { get; set; } = [];
    public required IEnumerable<Guid> GuidEnumerable { get; set; } = [];
    public required IEnumerable<Uri> UriEnumerable { get; set; } = [];
    public required IEnumerable<DateTime> DateEnumerable { get; set; } = [];
    public required IEnumerable<DateOnly> DateOnlyEnumerable { get; set; } = [];
    public required IEnumerable<TimeOnly> TimeOnlyEnumerable { get; set; } = [];
    public required IEnumerable<TimeSpan> TimeSpanEnumerable { get; set; } = [];
    public required IEnumerable<Sample> EnumEnumerable { get; set; } = [];
    public required IEnumerable<string> StringEnumerable { get; set; } = [];
    public required IEnumerable<SimpleItem> ItemEnumerable { get; set; } = [];

    public required ICollection<string> StringCollection { get; set; } = [];
    public required IList<Guid> GuidList { get; set; } = [];
    public required IReadOnlyCollection<bool> BoolReadOnlyCollection { get; set; } = [];
    public required IReadOnlyList<Sample> EnumReadOnlyList { get; set; } = [];
    public required List<SimpleItem> ItemList { get; set; } = [];
}
