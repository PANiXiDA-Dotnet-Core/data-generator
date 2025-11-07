namespace Tests.TestModels;

public sealed class DateTimeTestModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public DateTime Birthday { get; set; }
    public DateTime Dob { get; set; }

    public DateTime RegisteredAt { get; set; }
    public DateTime SignupDate { get; set; }

    public TimeSpan Duration { get; set; }

    public DateOnly BirthDateOnly { get; set; }
    public TimeOnly WakeUpTime { get; set; }
}
