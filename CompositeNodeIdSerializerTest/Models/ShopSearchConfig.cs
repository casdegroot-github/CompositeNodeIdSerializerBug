namespace CompositeNodeIdSerializerTest.Models;

public record ShopSearchConfig(
    string SearchQueryId,
    bool LtrEnabled,
    string[] Rewriters,
    string TestGroup,
    bool SkipMasterQueryMapping,
    bool SkipRedirect);