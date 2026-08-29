using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.Transport;

namespace FluentHub.Tests;

[TestClass]
public sealed class GitHubHttpClientTests
{
	[TestMethod]
	public async Task GetAsyncUsesSourceGeneratedMetadata()
	{
		var handler = new StubHttpMessageHandler(
			_ => JsonResponse(HttpStatusCode.OK, "{\"login\":\"octocat\"}"));
		using var httpClient = CreateHttpClient(handler);
		using var github = new GitHubHttpClient(httpClient);

		var user = await github.GetAsync("user", TransportTestJsonContext.Default.TransportTestUser);

		Assert.AreEqual("octocat", user.Login);
		Assert.AreEqual(HttpMethod.Get, handler.LastRequestMethod);
		Assert.AreEqual("https://api.github.test/user", handler.LastRequestUri);
	}

	[TestMethod]
	public async Task GetAsyncPreservesGitHubErrorDetails()
	{
		var reset = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds();
		var handler = new StubHttpMessageHandler(_ =>
		{
			var response = JsonResponse(
				HttpStatusCode.Forbidden,
				"{\"message\":\"API rate limit exceeded\",\"documentation_url\":\"https://docs.github.test/rate-limit\"}");
			response.Headers.TryAddWithoutValidation("X-RateLimit-Limit", "5000");
			response.Headers.TryAddWithoutValidation("X-RateLimit-Remaining", "0");
			response.Headers.TryAddWithoutValidation("X-RateLimit-Used", "5000");
			response.Headers.TryAddWithoutValidation("X-RateLimit-Reset", reset.ToString());
			response.Headers.TryAddWithoutValidation("X-RateLimit-Resource", "core");
			return response;
		});
		using var httpClient = CreateHttpClient(handler);
		using var github = new GitHubHttpClient(httpClient);

		var exception = await Assert.ThrowsExactlyAsync<GitHubApiException>(
			() => github.GetAsync("user", TransportTestJsonContext.Default.TransportTestUser));

		Assert.AreEqual(HttpStatusCode.Forbidden, exception.StatusCode);
		Assert.AreEqual("API rate limit exceeded", exception.Message);
		Assert.AreEqual("https://docs.github.test/rate-limit", exception.DocumentationUrl);
		Assert.AreEqual("https://api.github.test/user", exception.RequestUri?.ToString());
		Assert.AreEqual(5000, exception.RateLimit.Limit);
		Assert.AreEqual(0, exception.RateLimit.Remaining);
		Assert.AreEqual(5000, exception.RateLimit.Used);
		Assert.AreEqual(reset, exception.RateLimit.Reset?.ToUnixTimeSeconds());
		Assert.AreEqual("core", exception.RateLimit.Resource);
		StringAssert.Contains(exception.ResponseBody, "rate limit");
	}

	[TestMethod]
	public async Task ExecuteGraphQLAsyncWritesVariablesAndReadsTypedData()
	{
		var handler = new StubHttpMessageHandler(
			_ => JsonResponse(HttpStatusCode.OK, "{\"data\":{\"viewer\":{\"login\":\"octocat\"}}}"));
		using var httpClient = CreateHttpClient(handler);
		using var github = new GitHubHttpClient(httpClient);
		using var variablesDocument = JsonDocument.Parse("{\"includeName\":true}");

		var response = await github.ExecuteGraphQLAsync(
			"query Viewer($includeName: Boolean!) { viewer { login } }",
			variablesDocument.RootElement,
			TransportTestJsonContext.Default.GraphQLResponseTransportTestViewerData);

		Assert.AreEqual("octocat", response.Data?.Viewer?.Login);
		using var requestDocument = JsonDocument.Parse(handler.LastRequestBody);
		Assert.IsTrue(requestDocument.RootElement.GetProperty("variables").GetProperty("includeName").GetBoolean());
		StringAssert.Contains(requestDocument.RootElement.GetProperty("query").GetString(), "query Viewer");
	}

	[TestMethod]
	public async Task SendAsyncLeavesStatusHandlingToRawCallers()
	{
		var handler = new StubHttpMessageHandler(
			_ => JsonResponse(HttpStatusCode.NotFound, "{\"message\":\"Not Found\"}"));
		using var httpClient = CreateHttpClient(handler);
		using var github = new GitHubHttpClient(httpClient);
		using var request = new HttpRequestMessage(HttpMethod.Get, "missing");

		using var response = await github.SendAsync(request);

		Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
	}

	private static HttpClient CreateHttpClient(HttpMessageHandler handler)
		=> new(handler)
		{
			BaseAddress = new Uri("https://api.github.test/", UriKind.Absolute),
		};

	private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string content)
		=> new(statusCode)
		{
			Content = new StringContent(content, Encoding.UTF8, "application/json"),
		};

	private sealed class StubHttpMessageHandler(
		Func<HttpRequestMessage, HttpResponseMessage> responseFactory) : HttpMessageHandler
	{
		public string LastRequestBody { get; private set; } = string.Empty;
		public HttpMethod? LastRequestMethod { get; private set; }
		public string? LastRequestUri { get; private set; }

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			LastRequestMethod = request.Method;
			LastRequestUri = request.RequestUri?.ToString();
			if (request.Content is not null)
				LastRequestBody = await request.Content.ReadAsStringAsync(cancellationToken);

			return responseFactory(request);
		}
	}
}

internal sealed class TransportTestUser
{
	[JsonPropertyName("login")]
	public string? Login { get; init; }
}

internal sealed class TransportTestViewerData
{
	[JsonPropertyName("viewer")]
	public TransportTestUser? Viewer { get; init; }
}

[JsonSerializable(typeof(TransportTestUser))]
[JsonSerializable(typeof(GraphQLResponse<TransportTestViewerData>))]
internal sealed partial class TransportTestJsonContext : JsonSerializerContext;
