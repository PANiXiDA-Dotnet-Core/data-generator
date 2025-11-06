namespace DataGenerator.MutationProperties;

public interface IMutationIgnoreProperties<TId>
{
    TId Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
}
