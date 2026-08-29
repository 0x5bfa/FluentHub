// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

#nullable enable

using System;
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Transport;

namespace Octokit.GraphQL;

/// <summary>
/// Executes GitHub GraphQL operations without runtime code generation or reflection-based serialization.
/// </summary>
public sealed class GitHubGraphQLClient
{
	private readonly GitHubHttpClient _transport;

	public GitHubGraphQLClient(GitHubHttpClient transport)
	{
		ArgumentNullException.ThrowIfNull(transport);
		_transport = transport;
	}

	public Task<TData> ExecuteAsync<TData>(GraphQLOperation<TData> operation, JsonTypeInfo<TData> dataTypeInfo,
		Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default)
	{
		return ExecuteCoreAsync(operation.Document, dataTypeInfo, writeVariables, cancellationToken);
	}

	public Task<TData> ExecuteDynamicAsync<TData>(string query, JsonTypeInfo<TData> dataTypeInfo,
		Action<Utf8JsonWriter>? writeVariables = null, CancellationToken cancellationToken = default)
	{
		return ExecuteCoreAsync(query, dataTypeInfo, writeVariables, cancellationToken);
	}

	private async Task<TData> ExecuteCoreAsync<TData>(string query, JsonTypeInfo<TData> dataTypeInfo,
		Action<Utf8JsonWriter>? writeVariables, CancellationToken cancellationToken)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(query);
		ArgumentNullException.ThrowIfNull(dataTypeInfo);

		using var variablesDocument = CreateVariables(writeVariables);
		var response = await _transport.ExecuteGraphQLAsync(
			query,
			variablesDocument.RootElement,
			GraphQLJsonContext.Default.GraphQLResponseJsonElement,
			cancellationToken).ConfigureAwait(false);

		if (response.Errors is { Length: > 0 })
			throw new GraphQLException(response.Errors);

		if (response.Data is not { ValueKind: not (JsonValueKind.Null or JsonValueKind.Undefined) } data)
			throw new GraphQLException("GitHub returned a GraphQL response without data.");

		return JsonSerializer.Deserialize(data, dataTypeInfo)
			?? throw new GraphQLException("GitHub returned an incomplete GraphQL response.");
	}

	private static JsonDocument CreateVariables(Action<Utf8JsonWriter>? writeVariables)
	{
		var buffer = new ArrayBufferWriter<byte>();
		using (var writer = new Utf8JsonWriter(buffer))
		{
			writer.WriteStartObject();
			writeVariables?.Invoke(writer);
			writer.WriteEndObject();
		}

		return JsonDocument.Parse(buffer.WrittenMemory);
	}
}

[JsonSerializable(typeof(GraphQLResponse<JsonElement>))]
internal sealed partial class GraphQLJsonContext : JsonSerializerContext;
