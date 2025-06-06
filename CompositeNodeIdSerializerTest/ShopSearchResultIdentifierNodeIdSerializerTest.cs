using CompositeNodeIdSerializerTest.Models;
using HotChocolate.Types.Relay;
using NUnit.Framework;
using Shouldly;

namespace CompositeNodeIdSerializerTest;

[TestFixture]
public class ShopSearchResultIdentifierNodeIdSerializerTest
{
    private ShopSearchResultIdentifierNodeIdSerializer serializer = null!;

    [SetUp]
    public void SetUp() => serializer = new ShopSearchResultIdentifierNodeIdSerializer();

    [Test]
    public void Format_ShopSearchResultIdentifierWithAllParametersFilled_ShouldReturnCorrectNodeIdFormatterResult()
    {
        // Arrange
        var searchConfig = new ShopSearchConfig(
            SearchQueryId: "search123",
            LtrEnabled: true,
            Rewriters: new[] { "rewriter1", "rewriter2" },
            TestGroup: "testGroup1",
            SkipMasterQueryMapping: false,
            SkipRedirect: true);
        var identifier = new ShopSearchResultIdentifier("test query", searchConfig);
        var buffer = new byte[1024];

        // Act
        var result = serializer.Format(buffer, identifier, out var written);

        // Assert
        result.ShouldBe(NodeIdFormatterResult.Success);
        written.ShouldBeGreaterThan(0);

        var identifierString = System.Text.Encoding.UTF8.GetString(buffer.AsSpan(0, written));
        identifierString.ShouldNotBeNullOrEmpty();
        identifierString.ShouldBe(
            "{\"Query\":\"test query\",\"SearchQueryConfig\":{\"SearchQueryId\":\"search123\",\"LtrEnabled\":true,\"Rewriters\":[\"rewriter1\",\"rewriter2\"],\"TestGroup\":\"testGroup1\",\"SkipMasterQueryMapping\":false,\"SkipRedirect\":true}}");
    }

    [Test]
    public void TryParse_BufferWithAllParametersFilled_ShouldParseAllShopSearchResultIdentifierValuesCorrectly()
    {
        // Arrange
        var searchConfig = new ShopSearchConfig(
            SearchQueryId: "search123",
            LtrEnabled: true,
            Rewriters: new[] { "rewriter1", "rewriter2" },
            TestGroup: "testGroup1",
            SkipMasterQueryMapping: false,
            SkipRedirect: true);
        var originalIdentifier = new ShopSearchResultIdentifier("test query", searchConfig);
        var buffer = new byte[1024];
        var formatResult = serializer.Format(buffer, originalIdentifier, out var written);

        // Act
        var parseResult = serializer.TryParse(buffer.AsSpan(0, written), out var parsedObject);
        var parsedIdentifier = (ShopSearchResultIdentifier)parsedObject!;

        // Assert
        formatResult.ShouldBe(NodeIdFormatterResult.Success);
        parseResult.ShouldBeTrue();
        parsedIdentifier.Query.ShouldBe("test query");
        parsedIdentifier.SearchQueryConfig.ShouldNotBeNull();
        parsedIdentifier.SearchQueryConfig.SearchQueryId.ShouldBe("search123");
        parsedIdentifier.SearchQueryConfig.LtrEnabled.ShouldBe(true);
        parsedIdentifier.SearchQueryConfig.Rewriters.ShouldNotBeNull();
        parsedIdentifier.SearchQueryConfig.Rewriters.ShouldBe(new[] { "rewriter1", "rewriter2" });
        parsedIdentifier.SearchQueryConfig.TestGroup.ShouldBe("testGroup1");
        parsedIdentifier.SearchQueryConfig.SkipMasterQueryMapping.ShouldBe(false);
        parsedIdentifier.SearchQueryConfig.SkipRedirect.ShouldBe(true);
    }
}