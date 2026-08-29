// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Octokit.Transport;

public sealed class GitHubHttpClient : IDisposable
{
	public const string RestApiVersion = "2026-03-10";

	private readonly HttpClient _httpClient;
	private readonly bool _disposeHttpClient;
	private bool _disposed;

	public GitHubHttpClient(HttpClient httpClient, bool disposeHttpClient = false)
	{
		ArgumentNullException.ThrowIfNull(httpClient);

		_httpClient = httpClient;
		_disposeHttpClient = disposeHttpClient;
	}

	public static GitHubHttpClient Create(string accessToken, string productName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);
		ArgumentException.ThrowIfNullOrWhiteSpace(productName);

		var httpClient = new HttpClient
		{
			BaseAddress = new Uri("https://api.github.com/", UriKind.Absolute),
		};
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(productName);
		httpClient.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
		httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", RestApiVersion);

		return new GitHubHttpClient(httpClient, disposeHttpClient: true);
	}

	public async Task<T> GetAsync<T>(
		string relativeUri,
		JsonTypeInfo<T> responseTypeInfo,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(relativeUri);
		ArgumentNullException.ThrowIfNull(responseTypeInfo);

		using var request = new HttpRequestMessage(HttpMethod.Get, relativeUri);
		using var response = await SendSuccessfulAsync(request, cancellationToken).ConfigureAwait(false);
		await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

		return await JsonSerializer.DeserializeAsync(responseStream, responseTypeInfo, cancellationToken).ConfigureAwait(false)
			?? throw new InvalidOperationException($"GitHub returned an empty JSON response for '{relativeUri}'.");
	}

	public async Task<GraphQLResponse<TData>> ExecuteGraphQLAsync<TData>(
		string query,
		JsonElement variables,
		JsonTypeInfo<GraphQLResponse<TData>> responseTypeInfo,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(query);
		ArgumentNullException.ThrowIfNull(responseTypeInfo);
		if (variables.ValueKind is not (JsonValueKind.Undefined or JsonValueKind.Object))
			throw new ArgumentException("GraphQL variables must be a JSON object.", nameof(variables));

		using var payload = new MemoryStream();
		using (var writer = new Utf8JsonWriter(payload))
		{
			writer.WriteStartObject();
			writer.WriteString("query", query);
			writer.WritePropertyName("variables");
			if (variables.ValueKind == JsonValueKind.Undefined)
			{
				writer.WriteStartObject();
				writer.WriteEndObject();
			}
			else
			{
				variables.WriteTo(writer);
			}
			writer.WriteEndObject();
		}

		payload.Position = 0;
		using var request = new HttpRequestMessage(HttpMethod.Post, "graphql")
		{
			Content = new StreamContent(payload),
		};
		request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

		using var response = await SendSuccessfulAsync(request, cancellationToken).ConfigureAwait(false);
		await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

		return await JsonSerializer.DeserializeAsync(responseStream, responseTypeInfo, cancellationToken).ConfigureAwait(false)
			?? throw new InvalidOperationException("GitHub returned an empty GraphQL response.");
	}

	public async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken = default)
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		ArgumentNullException.ThrowIfNull(request);

		return await _httpClient.SendAsync(
			request,
			HttpCompletionOption.ResponseHeadersRead,
			cancellationToken).ConfigureAwait(false);
	}

	public void Dispose()
	{
		if (_disposed)
			return;

		_disposed = true;
		if (_disposeHttpClient)
			_httpClient.Dispose();
	}

	private async Task<HttpResponseMessage> SendSuccessfulAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		var response = await SendAsync(request, cancellationToken).ConfigureAwait(false);
		if (response.IsSuccessStatusCode)
			return response;

		throw await CreateExceptionAsync(response, cancellationToken).ConfigureAwait(false);
	}

	private static async Task<GitHubApiException> CreateExceptionAsync(
		HttpResponseMessage response,
		CancellationToken cancellationToken)
	{
		var statusCode = response.StatusCode;
		try
		{
			var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			var message = response.ReasonPhrase ?? $"GitHub API request failed with status {(int)statusCode}.";
			string? documentationUrl = null;

			if (!string.IsNullOrWhiteSpace(responseBody))
			{
				try
				{
					using var document = JsonDocument.Parse(responseBody);
					var root = document.RootElement;
					if (root.TryGetProperty("message", out var messageElement) &&
						messageElement.ValueKind == JsonValueKind.String)
					{
						message = messageElement.GetString() ?? message;
					}

					if (root.TryGetProperty("documentation_url", out var documentationElement) &&
						documentationElement.ValueKind == JsonValueKind.String)
					{
						documentationUrl = documentationElement.GetString();
					}
				}
				catch (JsonException)
				{
					// Preserve the HTTP status and raw response when GitHub returns a non-JSON body.
				}
			}

			return new GitHubApiException(statusCode, message, documentationUrl, responseBody);
		}
		finally
		{
			response.Dispose();
		}
	}
}
