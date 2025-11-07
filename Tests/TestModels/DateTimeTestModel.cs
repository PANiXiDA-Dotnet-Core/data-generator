namespace Tests.TestModels;

public sealed class DateTimeTestModel
{
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required DateTime? DeletedAt { get; set; }

    public required DateTime Birthday { get; set; }
    public required DateTime Dob { get; set; }

    public required DateTime RegisteredAt { get; set; }
    public required DateTime SignupDate { get; set; }

    public required TimeSpan Duration { get; set; }

    public required DateOnly BirthDateOnly { get; set; }
    public required TimeOnly WakeUpTime { get; set; }
}
