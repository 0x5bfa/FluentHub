using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.Rest;
using Octokit.Transport;

namespace FluentHub.Tests;

[TestClass]
public sealed class GitHubRestClientTests
{
	[TestMethod]
	public async Task AuthenticatedUserUsesTypedUserEndpoint()
	{
		var handler = new StubHttpMessageHandler(
			(_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"login\":\"octocat\"}")));
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubRestClient(transport);

		var user = await client.Users.GetAuthenticatedAsync();

		Assert.AreEqual("octocat", user.Login);
		Assert.AreEqual("https://api.github.test/user", handler.RequestUris.Single());
	}

	[TestMethod]
	public async Task RepositoryPaginationContinuesUntilPartialPage()
	{
		var fullPage = "[" + string.Join(',', Enumerable.Range(1, 100).Select(index => $"{{\"name\":\"branch-{index}\"}}")) + "]";
		var handler = new StubHttpMessageHandler((request, _) => Task.FromResult(JsonResponse(
			HttpStatusCode.OK,
			request.RequestUri?.Query.EndsWith("page=1", StringComparison.Ordinal) == true
				? fullPage
				: "[{\"name\":\"branch-101\"}]")));
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubRestClient(transport);

		var branches = await client.Repositories.GetBranchesAsync("owner", "repository");

		Assert.HasCount(101, branches);
		Assert.HasCount(2, handler.RequestUris);
		StringAssert.Contains(handler.RequestUris[0], "per_page=100&page=1");
		StringAssert.Contains(handler.RequestUris[1], "per_page=100&page=2");
	}

	[TestMethod]
	public async Task CancellationReachesUnderlyingHttpRequest()
	{
		var handler = new StubHttpMessageHandler(async (_, cancellationToken) =>
		{
			await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
			return JsonResponse(HttpStatusCode.OK, "{}");
		});
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubRestClient(transport);
		using var cancellation = new CancellationTokenSource();

		var request = client.Users.GetAuthenticatedAsync(cancellation.Token);
		cancellation.Cancel();

		await Assert.ThrowsExactlyAsync<TaskCanceledException>(() => request);
	}

	[TestMethod]
	public async Task ReadmeContentIsDecodedWithoutReflection()
	{
		var content = Convert.ToBase64String(Encoding.UTF8.GetBytes("# FluentHub"));
		var handler = new StubHttpMessageHandler(
			(_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK, $"{{\"content\":\"{content}\",\"encoding\":\"base64\"}}")));
		using var httpClient = CreateHttpClient(handler);
		using var transport = new GitHubHttpClient(httpClient);
		var client = new GitHubRestClient(transport);

		var readme = await client.Repositories.GetReadmeMarkdownAsync("owner", "repository");

		Assert.AreEqual("# FluentHub", readme);
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
		Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responseFactory) : HttpMessageHandler
	{
		public List<string> RequestUris { get; } = [];

		protected override Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			RequestUris.Add(request.RequestUri?.ToString() ?? string.Empty);
			return responseFactory(request, cancellationToken);
		}
	}
}
