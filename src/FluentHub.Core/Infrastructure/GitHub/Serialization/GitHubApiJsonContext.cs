// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Serialization
{
	[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
	[JsonSerializable(typeof(AuthenticatedUserResponse))]
	internal sealed partial class GitHubApiJsonContext : JsonSerializerContext
	{
	}

	internal sealed class AuthenticatedUserResponse
	{
		[JsonPropertyName("login")]
		public string? Login { get; init; }
	}
}
