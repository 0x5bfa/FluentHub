// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Octokit.Transport;

namespace Octokit.GraphQL;

public sealed class GraphQLException : Exception
{
	public GraphQLException(string message)
		: base(message)
	{
		Errors = [];
	}

	internal GraphQLException(IReadOnlyList<GraphQLError> errors, JsonElement? partialData = null)
		: base(CreateMessage(errors))
	{
		Errors = errors;
		if (partialData is { } data &&
			data.ValueKind is not (JsonValueKind.Null or JsonValueKind.Undefined))
		{
			PartialData = data.Clone();
		}
	}

	public IReadOnlyList<GraphQLError> Errors { get; }

	public JsonElement? PartialData { get; }

	private static string CreateMessage(IReadOnlyList<GraphQLError> errors)
		=> errors.Count == 0
			? "GitHub returned an unknown GraphQL error."
			: string.Join("; ", errors.Select(FormatError));

	private static string FormatError(GraphQLError error)
	{
		var prefix = string.IsNullOrWhiteSpace(error.Type) ? string.Empty : $"{error.Type}: ";
		var location = error.Locations is [{ } first, ..]
			? $" (line {first.Line}, column {first.Column})"
			: string.Empty;
		return prefix + error.Message + location;
	}
}
