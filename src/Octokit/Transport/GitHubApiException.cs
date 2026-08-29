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
		string responseBody)
		: base(message, inner: null, statusCode)
	{
		DocumentationUrl = documentationUrl;
		ResponseBody = responseBody;
	}

	public string? DocumentationUrl { get; }

	public string ResponseBody { get; }
}
