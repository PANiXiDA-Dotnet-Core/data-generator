using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class DataGeneratorRecursionTests
{
    [Fact(DisplayName = "Recursion depth default = 3")]
    public void Create_RecursionDepth_Default()
    {
        var generated = new DataFacade().Create<SampleModel>();

        CountDepth(generated).Should().Be(3);

        AssertNotEmptyCollections(generated);
        AssertNotEmptyDictionaries(generated);
    }

    [Fact(DisplayName = "Recursion depth = 2")]
    public void Create_RecursionDepth_2()
    {
        var generated = new DataFacade(recursionDepth: 2).Create<SampleModel>();

        CountDepth(generated).Should().Be(2);

        generated.Parent.Should().NotBeNull();
        generated.Parent.Parent.Should().BeNull();

        AssertNotEmptyCollections(generated);
        AssertNotEmptyDictionaries(generated);
    }

    [Fact(DisplayName = "Recursion depth = 1 (root only)")]
    public void Create_RecursionDepth_1()
    {
        var generated = new DataFacade(recursionDepth: 1).Create<SampleModel>();

        CountDepth(generated).Should().Be(1);
        generated.Parent.Should().BeNull();

        AssertEmptyCollections(generated);
        AssertEmptyDictionaries(generated);
    }

    [Fact(DisplayName = "Recursion depth = 0 behaves exactly as depth = 1")]
    public void Create_RecursionDepth_0()
    {
        var generated = new DataFacade(recursionDepth: 0).Create<SampleModel>();

        generated.Parent.Should().BeNull();

        AssertEmptyCollections(generated);
        AssertEmptyDictionaries(generated);
    }

    private static int CountDepth(SampleModel model)
    {
        var depth = 1;
        var current = model;

        while (current.Parent is not null)
        {
            depth++;
            current = current.Parent;
        }

        return depth;
    }

    private static void AssertNotEmptyCollections(SampleModel generated)
    {
        generated.Children.Should().NotBeNull().And.NotBeEmpty();
        generated.Siblings.Should().NotBeNull().And.NotBeEmpty();
        generated.EnumerableChildren.Should().NotBeNull().And.NotBeEmpty();
        generated.CollectionChildren.Should().NotBeNull().And.NotBeEmpty();
        generated.ListChildren.Should().NotBeNull().And.NotBeEmpty();
        generated.ReadOnlyCollectionChildren.Should().NotBeNull().And.NotBeEmpty();
        generated.ReadOnlyListChildren.Should().NotBeNull().And.NotBeEmpty();
    }

    private static void AssertEmptyCollections(SampleModel generated)
    {
        generated.Children.Should().NotBeNull().And.BeEmpty();
        generated.Siblings.Should().NotBeNull().And.BeEmpty();
        generated.EnumerableChildren.Should().NotBeNull().And.BeEmpty();
        generated.CollectionChildren.Should().NotBeNull().And.BeEmpty();
        generated.ListChildren.Should().NotBeNull().And.BeEmpty();
        generated.ReadOnlyCollectionChildren.Should().NotBeNull().And.BeEmpty();
        generated.ReadOnlyListChildren.Should().NotBeNull().And.BeEmpty();
    }

    private static void AssertNotEmptyDictionaries(SampleModel generated)
    {
        generated.Related.Should().NotBeNull().And.NotBeEmpty();
        generated.ReadOnlyRelated.Should().NotBeNull().And.NotBeEmpty();
        generated.InterfaceDictionaryRelated.Should().NotBeNull().And.NotBeEmpty();
        generated.SortedRelated.Should().NotBeNull().And.NotBeEmpty();
        generated.ConcurrentRelated.Should().NotBeNull().And.NotBeEmpty();
    }

    private static void AssertEmptyDictionaries(SampleModel generated)
    {
        generated.Related.Should().NotBeNull().And.BeEmpty();
        generated.ReadOnlyRelated.Should().NotBeNull().And.BeEmpty();
        generated.InterfaceDictionaryRelated.Should().NotBeNull().And.BeEmpty();
        generated.SortedRelated.Should().NotBeNull().And.BeEmpty();
        generated.ConcurrentRelated.Should().NotBeNull().And.BeEmpty();
    }
}