// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;

namespace FluentHub.Core.Infrastructure.GitHub.Serialization;

internal static class GraphQLInputWriter
{
	public static void WritePage(Utf8JsonWriter writer, PageRequest page)
	{
		ArgumentNullException.ThrowIfNull(page);
		if (page.First is not null)
			writer.WriteNumber("first", page.First.Value);
		WriteOptionalString(writer, "after", page.After);
		if (page.Last is not null)
			writer.WriteNumber("last", page.Last.Value);
		WriteOptionalString(writer, "before", page.Before);
	}

	public static void WriteOptionalString(Utf8JsonWriter writer, string propertyName, string? value)
	{
		if (value is not null)
			writer.WriteString(propertyName, value);
	}

	public static void WriteOptionalBoolean(Utf8JsonWriter writer, string propertyName, bool? value)
	{
		if (value is not null)
			writer.WriteBoolean(propertyName, value.Value);
	}

	public static void WriteOptionalId(Utf8JsonWriter writer, string propertyName, ID? value)
	{
		if (value is not null)
			writer.WriteString(propertyName, value.Value.Value);
	}

	public static void WriteOptionalIds(Utf8JsonWriter writer, string propertyName, IReadOnlyList<ID>? values)
	{
		if (values is null)
			return;

		writer.WriteStartArray(propertyName);
		foreach (var value in values)
			writer.WriteStringValue(value.Value);
		writer.WriteEndArray();
	}

	public static void WriteOptionalStrings(Utf8JsonWriter writer, string propertyName, IEnumerable<string>? values)
	{
		if (values is null)
			return;
		writer.WriteStartArray(propertyName);
		foreach (var value in values)
			writer.WriteStringValue(value);
		writer.WriteEndArray();
	}
}
