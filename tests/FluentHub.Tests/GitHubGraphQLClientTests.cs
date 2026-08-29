using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;
using Octokit.Transport;

namespace FluentHub.Tests;

[TestClass]
public sealed class GitHubGraphQLClientTests
{
	[TestMethod]
	public async Task ExecuteAsyncWritesVariablesAndReadsTypedData()
	{
		var handler = new StubHttpMessageHandler(
			"{\"data\":{\"viewer\":{\"login\":\"octocat\"}}}");
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubGraphQLClient(transport);

		var operation = new GraphQLOperation<GraphQLClientTestData>(
			"query Viewer($includeName: Boolean!) { viewer { login } }", "Viewer", GraphQLOperationType.Query);
		var result = await client.ExecuteAsync(
			operation,
			GraphQLClientTestJsonContext.Default.GraphQLClientTestData,
			writer => writer.WriteBoolean("includeName", true));

		Assert.AreEqual("octocat", result.Viewer?.Login);
		using var requestDocument = JsonDocument.Parse(handler.LastRequestBody);
		Assert.IsTrue(requestDocument.RootElement
			.GetProperty("variables")
			.GetProperty("includeName")
			.GetBoolean());
	}

	[TestMethod]
	public async Task ExecuteDynamicAsyncPreservesGraphQLErrorDetails()
	{
		var handler = new StubHttpMessageHandler(
			"{\"errors\":[{\"message\":\"Resource not accessible\",\"type\":\"FORBIDDEN\",\"locations\":[{\"line\":2,\"column\":3}]}]}");
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubGraphQLClient(transport);

		var exception = await Assert.ThrowsExactlyAsync<GraphQLException>(() => client.ExecuteDynamicAsync(
			"query { viewer { login } }",
			GraphQLClientTestJsonContext.Default.GraphQLClientTestData));

		Assert.HasCount(1, exception.Errors);
		Assert.AreEqual("Resource not accessible", exception.Errors[0].Message);
		Assert.AreEqual("FORBIDDEN", exception.Errors[0].Type);
		Assert.AreEqual(2, exception.Errors[0].Locations?[0].Line);
		Assert.AreEqual("FORBIDDEN: Resource not accessible (line 2, column 3)", exception.Message);
	}

	private static HttpClient CreateHttpClient(HttpMessageHandler handler)
	{
		return new(handler)
		{
			BaseAddress = new Uri("https://api.github.test/", UriKind.Absolute),
		};
	}

	private sealed class StubHttpMessageHandler(string responseBody) : HttpMessageHandler
	{
		public string LastRequestBody { get; private set; } = string.Empty;

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			LastRequestBody = request.Content is null
				? string.Empty
				: await request.Content.ReadAsStringAsync(cancellationToken);

			return new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(responseBody, Encoding.UTF8, "application/json"),
			};
		}
	}
}

internal sealed class GraphQLClientTestData
{
	public GraphQLClientTestViewer? Viewer { get; init; }
}

internal sealed class GraphQLClientTestViewer
{
	public string? Login { get; init; }
}

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(GraphQLClientTestData))]
internal sealed partial class GraphQLClientTestJsonContext : JsonSerializerContext;
