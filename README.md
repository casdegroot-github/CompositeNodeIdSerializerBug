# HotChocolate Node ID Serialization Bug Reproduction

This repository demonstrates a bug in HotChocolate's Node serialization where the ID format has changed between versions, breaking escape character handling.

## Bug Description

Node serialization has been changed, where now the ID does not have escape characters anymore when using `CompositeNodeIdValueSerializer`.

## Setup

- **HotChocolate Version**: 15.1.6-p.1 (working) / 15.1.6-p.2 (broken)
- **Target Framework**: .NET 9.0
- **Test Framework**: NUnit with Shouldly assertions

## Version Behavior

- ✅ **15.1.6-p.1**: Tests pass - Node ID serialization works as expected
- ❌ **15.1.6-p.2**: Tests fail - Node ID serialization format changed, breaking existing behavior

## Structure

- `CompositeNodeIdSerializerTest/` - Main test project
  - `Models/` - Data models (`ShopSearchConfig`, `ShopSearchResultIdentifier`)
  - `ShopSearchResultIdentifierNodeIdSerializer.cs` - Custom composite node ID serializer
  - `ShopSearchResultIdentifierNodeIdSerializerTest.cs` - Tests demonstrating the issue

## Running Tests

```bash
dotnet test
```

## Expected Behavior

The serializer should properly format and parse composite node IDs with consistent escape character handling across HotChocolate versions.

## Current Issue

When upgrading from HotChocolate 15.1.6-p.1 to 15.1.6-p.2, the node ID serialization format changes, causing tests that verify the exact serialized format to fail. The specific change affects how escape characters are handled in the serialized output.

### Reproduction Steps

1. Run tests with HotChocolate 15.1.6-p.1 - ✅ Tests pass
2. Update package reference to 15.1.6-p.2
3. Run tests again - ❌ Tests fail due to changed serialization format

## Test Case

The test verifies that:

1. A complex object with nested properties serializes correctly
2. The serialized format can be parsed back to the original object
3. All properties are preserved during the serialization round-trip

The failing assertion checks that the serialized string matches the expected JSON format exactly, including escape characters.
