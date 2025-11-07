using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class NullableTests
{
    [Fact(DisplayName = "Nullable fields should sometimes be null and sometimes have values")]
    public void Nullable_Generation_CanBeNull_And_NotNull()
    {
        var facade = new DataFacade();

        var middleNames = new List<string?>();
        var deletedDates = new List<DateTime?>();

        for (int i = 0; i < 10; i++)
        {
            var generated = facade.Create<NullableModel>();
            middleNames.Add(generated.MiddleName);
            deletedDates.Add(generated.DeletedAt);
        }

        middleNames.Should().Contain(item => item == null);
        middleNames.Should().Contain(item => item != null);

        deletedDates.Should().Contain(item => item == null);
        deletedDates.Should().Contain(item => item != null);
    }
}
