using DataGenerator.MutationProperties;

namespace Tests.TestModels;

public sealed class SampleModel : IMutationIgnoreProperties<int>, IMutationForceProperties
{
    public required int Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public required Guid ExternalId { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required int Age { get; set; }
    public required bool IsActive { get; set; }
    public required Uri AvatarUrl { get; set; }
    public required Sample Status { get; set; }

    public required int[] Scores { get; set; } = [];
    public required List<string> Tags { get; set; } = [];
    public required Dictionary<string, int> Metadata { get; set; } = [];
    public required IDictionary<string, int> Metadata2 { get; set; }
    public required IReadOnlyDictionary<string, int> Metadata3 { get; set; }
}

public enum Sample
{
    Unknown = 0,
    Active,
    Disabled
}