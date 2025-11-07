namespace Tests.TestModels;

public sealed class NumericTestModel
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int Age { get; set; }
    public required int ItemCount { get; set; }
    public required int Qty { get; set; }
    public required int TotalNum { get; set; }
    public required int Price { get; set; }
    public required int OrderAmount { get; set; }
    public required int TotalSum { get; set; }
    public required int ReleaseYear { get; set; }

    public required uint UIntValue { get; set; }
    public required long LongValue { get; set; }
    public required ulong ULongValue { get; set; }
    public required short ShortValue { get; set; }
    public required ushort UShortValue { get; set; }
    public required byte ByteValue { get; set; }
    public required sbyte SByteValue { get; set; }
    public required double DoubleValue { get; set; }
    public required float FloatValue { get; set; }
    public required decimal DecimalValue { get; set; }
}
