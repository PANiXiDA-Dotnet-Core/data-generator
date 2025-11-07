using Tests.TestModels.Common;
using Tests.TestModels.Common.Enums;

namespace Tests.TestModels;

public sealed class ArrayTestModel
{
    public required int[] Ints { get; set; } = [];
    public required double[] Doubles { get; set; } = [];
    public required bool[] Bools { get; set; } = [];
    public required char[] Chars { get; set; } = [];
    public required Guid[] Guids { get; set; } = [];
    public required Uri[] Uris { get; set; } = [];
    public required DateTime[] Dates { get; set; } = [];
    public required DateOnly[] DateOnlys { get; set; } = [];
    public required TimeOnly[] TimeOnlys { get; set; } = [];
    public required TimeSpan[] TimeSpans { get; set; } = [];
    public required Sample[] Enums { get; set; } = [];
    public required string[] Strings { get; set; } = [];
    public required SimpleItem[] Items { get; set; } = [];
}
