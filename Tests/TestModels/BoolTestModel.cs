namespace Tests.TestModels;

public sealed class BoolTestModel
{
    public required bool IsActive { get; set; }
    public required bool HasAccess { get; set; }
    public required bool FeatureEnabled { get; set; }
    public required bool IsDeleted { get; set; }
    public required bool IsDisabled { get; set; }
    public required bool IsBlocked { get; set; }
    public required bool RandomFlag { get; set; }
}
