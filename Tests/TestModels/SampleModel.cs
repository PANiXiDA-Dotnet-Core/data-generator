using System.Collections.Concurrent;

using DataGenerator.MutationProperties;

using Tests.TestModels.Common.Enums;

namespace Tests.TestModels;

public sealed class SampleModel : IMutationIgnoreProperties<int>, IMutationForceProperties
{
    public required int Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public required string Login { get; set; }
    public required string Password { get; set; }

    public required Guid ExternalId { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required int Age { get; set; }
    public required bool IsActive { get; set; }
    public required Uri AvatarUrl { get; set; }
    public required Sample Status { get; set; }

    public required int[] Scores { get; set; } = [];

    public required SampleModel Parent { get; set; } = null!;

    public required SampleModel[] Siblings { get; set; } = [];

    public required List<SampleModel> Children { get; set; } = [];
    public required IEnumerable<SampleModel> EnumerableChildren { get; set; } = [];
    public required ICollection<SampleModel> CollectionChildren { get; set; } = [];
    public required IList<SampleModel> ListChildren { get; set; } = [];
    public required IReadOnlyCollection<SampleModel> ReadOnlyCollectionChildren { get; set; } = [];
    public required IReadOnlyList<SampleModel> ReadOnlyListChildren { get; set; } = [];

    public required Dictionary<string, SampleModel> Related { get; set; } = [];
    public required IReadOnlyDictionary<string, SampleModel> ReadOnlyRelated { get; set; } = new Dictionary<string, SampleModel>();
    public required IDictionary<string, SampleModel> InterfaceDictionaryRelated { get; set; } = new Dictionary<string, SampleModel>();
    public required SortedDictionary<string, SampleModel> SortedRelated { get; set; } = [];
    public required ConcurrentDictionary<string, SampleModel> ConcurrentRelated { get; set; } = [];
}