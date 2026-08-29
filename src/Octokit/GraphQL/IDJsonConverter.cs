// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Octokit.GraphQL;

public sealed class IDJsonConverter : JsonConverter<ID>
{
	public override ID Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> reader.TokenType == JsonTokenType.String
			? new ID(reader.GetString() ?? string.Empty)
			: throw new JsonException("A GitHub node ID must be a JSON string.");

	public override void Write(Utf8JsonWriter writer, ID value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.Value);
}
