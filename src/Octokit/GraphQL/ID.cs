// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;

namespace Octokit.GraphQL;

/// <summary>
/// Represents a GitHub GraphQL node identifier.
/// </summary>
[JsonConverter(typeof(IDJsonConverter))]
public readonly record struct ID(string Value)
{
	public override string ToString() => Value ?? string.Empty;
}
