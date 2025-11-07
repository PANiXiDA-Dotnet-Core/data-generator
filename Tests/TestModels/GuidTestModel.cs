namespace Tests.TestModels;

public sealed class GuidTestModel
{
    public required Guid Id { get; set; }
    public required Guid ExternalId { get; set; }
    public required Guid CorrelationId { get; set; }
}
