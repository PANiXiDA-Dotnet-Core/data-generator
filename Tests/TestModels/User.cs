using DataGenerator.MutationProperties;

namespace Tests.TestModels;

public sealed class User : IMutationIgnoreProperties<int>, IMutationForceProperties
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public required string Login { get; set; }
    public required string Password { get; set; }

    public required string Email { get; set; }
    public required string DisplayName { get; set; }
}
