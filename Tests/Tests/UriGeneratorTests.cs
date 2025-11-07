using DataGenerator;

using FluentAssertions;

using Tests.TestModels;

using Xunit;

namespace Tests.Tests;

public sealed class UriGeneratorTests
{
    [Fact(DisplayName = "UriGenerator should generate valid absolute URIs")]
    public void Create_UriProperties()
    {
        var generated = new DataFacade().Create<UriTestModel>();

        generated.WebsiteUri.Should().NotBeNull();
        generated.AvatarUri.Should().NotBeNull();
        generated.ApiEndpointUri.Should().NotBeNull();

        Uri.IsWellFormedUriString(generated.WebsiteUri.ToString(), UriKind.Absolute).Should().BeTrue();
        Uri.IsWellFormedUriString(generated.AvatarUri.ToString(), UriKind.Absolute).Should().BeTrue();
        Uri.IsWellFormedUriString(generated.ApiEndpointUri.ToString(), UriKind.Absolute).Should().BeTrue();
    }
}
