// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Net;

namespace Octokit.Transport;

public sealed class GitHubApiException : HttpRequestException
{
	internal GitHubApiException(
		HttpStatusCode statusCode,
		string message,
		string? documentationUrl,
		string responseBody,
		Uri? requestUri,
		GitHubRateLimit rateLimit,
		TimeSpan? retryAfter)
		: base(message, inner: null, statusCode)
	{
		DocumentationUrl = documentationUrl;
		ResponseBody = responseBody;
		RequestUri = requestUri;
		RateLimit = rateLimit;
		RetryAfter = retryAfter;
	}

	public string? DocumentationUrl { get; }

	public string ResponseBody { get; }

	public Uri? RequestUri { get; }

	public GitHubRateLimit RateLimit { get; }

	public TimeSpan? RetryAfter { get; }
}

public readonly record struct GitHubRateLimit(
	int? Limit,
	int? Remaining,
	int? Used,
	DateTimeOffset? Reset,
	string? Resource);
