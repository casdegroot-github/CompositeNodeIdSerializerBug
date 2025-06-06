# HotChocolate Node ID Serialization Bug Reproduction

This repository demonstrates a bug in HotChocolate's Node serialization where the ID format has changed between versions, breaking escape character handling.

## Bug Description

Node serialization has been changed, where now the ID does not have escape characters anymore when using `CompositeNodeIdValueSerializer`.

## Setup

- **HotChocolate Version**: 15.1.6-p.1
- **Target Framework**: .NET 9.0
- **Test Framework**: NUnit with Shouldly assertions

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

When upgrading to newer versions of HotChocolate, the node ID serialization format changes, causing tests that verify the exact serialized format to fail.

## Test Case

The test verifies that:
1. A complex object with nested properties serializes correctly
2. The serialized format can be parsed back to the original object
3. All properties are preserved during the serialization round-trip

The failing assertion checks that the serialized string matches the expected JSON format exactly, including escape characters.
