using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using CompositeNodeIdSerializerTest.Models;
using HotChocolate.Types.Relay;

namespace CompositeNodeIdSerializerTest;

public class ShopSearchResultIdentifierNodeIdSerializerSingle : INodeIdValueSerializer
{
    public NodeIdFormatterResult Format(Span<byte> buffer, object value, out int written)
    {
        if (value is not ShopSearchResultIdentifier identifier)
        {
            written = 0;
            return NodeIdFormatterResult.InvalidValue;
        }

        var serializedIdentifier = JsonSerializer.Serialize(identifier);

        if (buffer.Length < serializedIdentifier.Length)
        {
            written = 0;
            return NodeIdFormatterResult.BufferTooSmall;
        }

        if (Encoding.UTF8.TryGetBytes(serializedIdentifier, buffer, out var bytesWritten))
        {
            written = bytesWritten;
            return NodeIdFormatterResult.Success;
        }

        written = 0;
        return NodeIdFormatterResult.BufferTooSmall;
    }

    public bool IsSupported(Type type) => type == typeof(ShopSearchResultIdentifier);

    public bool TryParse(ReadOnlySpan<byte> buffer, [NotNullWhen(true)] out object? value)
    {

        try
        {
            var identifier = JsonSerializer.Deserialize<ShopSearchResultIdentifier>(buffer);
            if (identifier is null)
            {
                value = default!;
                return false;
            }

            value = identifier;
            return true;
        }
        catch
        {
            value = default!;
            return false;
        }
    }
}