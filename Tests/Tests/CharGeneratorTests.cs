using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class CharGeneratorTests
{
    private const int SampleCount = 200;
    private const int MajorityThreshold = SampleCount * 60 / 100;

    [Fact(DisplayName = "CharGenerator should generate mostly lowercase letters")]
    public void Create_MostlyLetters()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<CharTestModel>().Letter)
            .ToList();

        var lettersCount = generated.Count(item => item is >= 'a' and <= 'z');
        lettersCount.Should().BeGreaterThan(MajorityThreshold);
    }

    [Fact(DisplayName = "CharGenerator should generate some digits")]
    public void Create_ShouldGenerateDigits()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<CharTestModel>().Digit)
            .ToList();

        var digitsCount = generated.Count(item => item is >= '0' and <= '9');
        digitsCount.Should().BeGreaterThan(0);
        digitsCount.Should().BeLessThan(MajorityThreshold);
    }

    [Fact(DisplayName = "CharGenerator should generate some printable non-alphanumeric symbols")]
    public void Create_ShouldGenerateSpecialCharacters()
    {
        var facade = new DataFacade();
        var generated = Enumerable.Range(0, SampleCount)
            .Select(_ => facade.Create<CharTestModel>().Any)
            .ToList();

        var specialCount = generated.Count(item =>
            item is >= '\u0021' and <= '\u007E'
            && (item < 'a' || item > 'z')
            && (item < '0' || item > '9')
        );

        specialCount.Should().BeGreaterThan(0);
        specialCount.Should().BeLessThan(SampleCount / 3);
    }
}
