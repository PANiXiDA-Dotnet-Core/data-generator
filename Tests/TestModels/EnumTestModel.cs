using Tests.TestModels.Common.Enums;

namespace Tests.TestModels;

public sealed class EnumTestModel
{
    public required Basic Basic { get; set; }
    public required NoZero NoZero { get; set; }
    public required Weird Weird { get; set; }
}
