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
        var generated = facade.Create<SampleModel>();

        var mutated = facade.Mutate(generated);

        mutated.Should().NotBeEquivalentTo(generated);
    }

    [Fact(DisplayName = "Mutate should not change system fields (Id, CreatedAt, UpdatedAt, DeletedAt)")]
    public void Mutate_DoesNotChangeSystemFields()
    {
        var facade = new DataFacade();
        var generated = facade.Create<SampleModel>();

        var mutated = facade.Mutate(generated);

        mutated.Id.Should().Be(generated.Id);
        mutated.CreatedAt.Should().Be(generated.CreatedAt);
        mutated.UpdatedAt.Should().Be(generated.UpdatedAt);
        mutated.DeletedAt.Should().Be(generated.DeletedAt);
    }

    [Fact(DisplayName = "Mutate should respect explicitly excluded properties")]
    public void Mutate_RespectsExcludeProperties()
    {
        var facade = new DataFacade();
        var generated = facade.Create<SampleModel>();

        var mutated = facade.Mutate(generated, excludeProperties: [nameof(SampleModel.Email)]);

        mutated.Email.Should().Be(generated.Email);
    }

    [Fact(DisplayName = "Mutate should always change Login and Password")]
    public void Mutate_AlwaysChangesLoginAndPassword()
    {
        var facade = new DataFacade();
        var generated = facade.Create<SampleModel>();

        var mutated = facade.Mutate(generated);

        mutated.Login.Should().NotBe(generated.Login);
        mutated.Password.Should().NotBe(generated.Password);
    }
}
