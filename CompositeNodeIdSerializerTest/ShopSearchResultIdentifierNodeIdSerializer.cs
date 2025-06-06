using System.Text.Json;
using CompositeNodeIdSerializerTest.Models;
using HotChocolate.Types.Relay;

namespace CompositeNodeIdSerializerTest;

public class ShopSearchResultIdentifierNodeIdSerializer
    : CompositeNodeIdValueSerializer<ShopSearchResultIdentifier>
{
    protected override NodeIdFormatterResult Format(
        Span<byte> buffer,
        ShopSearchResultIdentifier value,
        out int written)
    {
        var serializedIdentifier = JsonSerializer.Serialize(value);

        if (TryFormatIdPart(buffer, serializedIdentifier, out var identifierLength))
        {
            written = identifierLength;

            return NodeIdFormatterResult.Success;
        }

        written = 0;

        return NodeIdFormatterResult.BufferTooSmall;
    }

    protected override bool TryParse(
        ReadOnlySpan<byte> buffer,
        out ShopSearchResultIdentifier value)
    {
        try
        {
            var identifier = JsonSerializer.Deserialize<ShopSearchResultIdentifier?>(buffer);
            if (identifier is null)
            {
                value = default;
                return false;
            }

            value = identifier;

            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }
}