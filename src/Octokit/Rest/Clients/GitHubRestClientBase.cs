// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json.Serialization.Metadata;
using Octokit.Transport;

namespace Octokit.Rest;

public abstract class GitHubRestClientBase
{
	protected GitHubRestClientBase(GitHubHttpClient transport)
		=> Transport = transport ?? throw new ArgumentNullException(nameof(transport));

	protected GitHubHttpClient Transport { get; }

	protected static string Segment(string value, string parameterName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
		return Uri.EscapeDataString(value.Trim());
	}

	protected static string QueryValue(string value, string parameterName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
		return Uri.EscapeDataString(value.Trim());
	}

	protected async Task<IReadOnlyList<T>> GetAllPagesAsync<T>(
		Func<int, string> createRelativeUri,
		JsonTypeInfo<List<T>> responseTypeInfo,
		CancellationToken cancellationToken)
	{
		const int pageSize = 100;
		var result = new List<T>();

		for (var page = 1; ; page++)
		{
			var items = await Transport.GetAsync(
				createRelativeUri(page),
				responseTypeInfo,
				cancellationToken).ConfigureAwait(false);
			result.AddRange(items);

			if (items.Count < pageSize)
				return result;
		}
	}
}
