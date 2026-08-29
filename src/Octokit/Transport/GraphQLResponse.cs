// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Octokit.Transport;

public sealed class GraphQLResponse<TData>
{
	[JsonPropertyName("data")]
	public TData? Data { get; init; }

	[JsonPropertyName("errors")]
	public GraphQLError[]? Errors { get; init; }

	[JsonPropertyName("extensions")]
	public JsonElement Extensions { get; init; }
}

public sealed class GraphQLError
{
	[JsonPropertyName("message")]
	public string Message { get; init; } = string.Empty;

	[JsonPropertyName("type")]
	public string? Type { get; init; }

	[JsonPropertyName("locations")]
	public GraphQLErrorLocation[]? Locations { get; init; }

	[JsonPropertyName("path")]
	public JsonElement Path { get; init; }

	[JsonPropertyName("extensions")]
	public JsonElement Extensions { get; init; }
}

public sealed class GraphQLErrorLocation
{
	[JsonPropertyName("line")]
	public int Line { get; init; }

	[JsonPropertyName("column")]
	public int Column { get; init; }
}
