using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests;

public sealed class MutateTests
{
    [Fact(DisplayName = "Mutate should change at least one property")]
    public void Mutate_ChangesAtLeastOneProperty()
    {
        var facade = new DataFacade();
        var user = facade.Create<User>();

        var mutated = facade.Mutate(user);

        mutated.Should().NotBeEquivalentTo(user);
    }

    [Fact(DisplayName = "Mutate should not change system fields (Id, CreatedAt, UpdatedAt, DeletedAt)")]
    public void Mutate_DoesNotChangeSystemFields()
    {
        var facade = new DataFacade();
        var user = facade.Create<User>();

        var mutated = facade.Mutate(user);

        mutated.Id.Should().Be(user.Id);
        mutated.CreatedAt.Should().Be(user.CreatedAt);
        mutated.UpdatedAt.Should().Be(user.UpdatedAt);
        mutated.DeletedAt.Should().Be(user.DeletedAt);
    }

    [Fact(DisplayName = "Mutate should respect explicitly excluded properties")]
    public void Mutate_RespectsExcludeProperties()
    {
        var facade = new DataFacade();
        var user = facade.Create<User>();

        var mutated = facade.Mutate(user, excludeProperties: [nameof(User.Email)]);

        mutated.Email.Should().Be(user.Email);
    }

    [Fact(DisplayName = "Mutate should always change Login and Password")]
    public void Mutate_AlwaysChangesLoginAndPassword()
    {
        var facade = new DataFacade();
        var user = facade.Create<User>();

        var mutated = facade.Mutate(user);

        mutated.Login.Should().NotBe(user.Login);
        mutated.Password.Should().NotBe(user.Password);
    }
}
