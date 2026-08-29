using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.Rest;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using GitHubApiException = Octokit.Transport.GitHubApiException;
using GitHubHttpClient = Octokit.Transport.GitHubHttpClient;

namespace FluentHub.Tests;

[TestClass]
public sealed class ForkRepositoryMutationTests
{
	[TestMethod]
	public async Task CreateForkSendsCustomDestinationNameAndBranchSelection()
	{
		using var api = new FakeGitHubApiClient();
		var mutation = new ForkRepositoryMutation(api);

		var result = await mutation.ExecuteAsync(new CreateForkRequest
		{
			DefaultBranchOnly = true,
			DestinationOwner = new ForkOwner
			{
				IsOrganization = true,
				Login = "destination-org",
			},
			RepositoryName = "renamed-fork",
			SourceName = "source-repo",
			SourceOwner = "source-owner",
		});

		Assert.AreEqual(HttpMethod.Post, api.RequestMethod);
		Assert.AreEqual("repos/source-owner/source-repo/forks", api.RequestUri);
		Assert.IsTrue(api.AcceptsGitHubJson);
		Assert.AreEqual(global::Octokit.Transport.GitHubHttpClient.RestApiVersion, api.ApiVersion);

		using var document = JsonDocument.Parse(api.RequestBody);
		var root = document.RootElement;
		Assert.AreEqual("destination-org", root.GetProperty("organization").GetString());
		Assert.AreEqual("renamed-fork", root.GetProperty("name").GetString());
		Assert.IsTrue(root.GetProperty("default_branch_only").GetBoolean());
		Assert.AreEqual("destination-org/renamed-fork", result.FullName);
	}

	[TestMethod]
	public async Task CreateForkOmitsOrganizationForPersonalDestination()
	{
		using var api = new FakeGitHubApiClient();
		var mutation = new ForkRepositoryMutation(api);

		await mutation.ExecuteAsync(new CreateForkRequest
		{
			DestinationOwner = new ForkOwner { Login = "viewer" },
			RepositoryName = "source-repo",
			SourceName = "source-repo",
			SourceOwner = "source-owner",
		});

		using var document = JsonDocument.Parse(api.RequestBody);
		Assert.IsFalse(document.RootElement.TryGetProperty("organization", out _));
	}

	[TestMethod]
	public async Task CreateForkRejectsMissingRequest()
	{
		var mutation = new ForkRepositoryMutation(null!);
		await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => mutation.ExecuteAsync(null!));
	}

	[TestMethod]
	public async Task CreateForkIncludesGitHubValidationDetailsInError()
	{
		using var api = new FakeGitHubApiClient
		{
			ResponseBody = "{\"message\":\"Validation Failed\",\"errors\":[{\"field\":\"name\",\"code\":\"already_exists\"}]}",
			StatusCode = HttpStatusCode.UnprocessableEntity,
		};
		var mutation = new ForkRepositoryMutation(api);

		var exception = await Assert.ThrowsExactlyAsync<GitHubApiException>(() =>
			mutation.ExecuteAsync(new CreateForkRequest
			{
				DestinationOwner = new ForkOwner { Login = "viewer" },
				RepositoryName = "source-repo",
				SourceName = "source-repo",
				SourceOwner = "source-owner",
			}));

		StringAssert.Contains(exception.Message, "name: already_exists");
	}

	private sealed class FakeGitHubApiClient : IGitHubApiClient, IDisposable
	{
		private readonly HttpClient _httpClient;
		private readonly GitHubHttpClient _transport;
		private readonly GitHubRestClient _rest;

		public FakeGitHubApiClient()
		{
			_httpClient = new HttpClient(new StubHttpMessageHandler(this))
			{
				BaseAddress = new Uri("https://api.github.test/", UriKind.Absolute),
			};
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
			_httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", GitHubHttpClient.RestApiVersion);
			_transport = new GitHubHttpClient(_httpClient);
			_rest = new GitHubRestClient(_transport);
		}

		public string ApiVersion { get; private set; } = string.Empty;
		public bool AcceptsGitHubJson { get; private set; }
		public string RequestBody { get; private set; } = string.Empty;
		public HttpMethod? RequestMethod { get; private set; }
		public string RequestUri { get; private set; } = string.Empty;
		public string ResponseBody { get; init; } =
			"{\"name\":\"renamed-fork\",\"full_name\":\"destination-org/renamed-fork\",\"owner\":{\"login\":\"destination-org\"}}";
		public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.Accepted;

		public Task<T> RunRestAsync<T>(
			Func<GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
			=> operation(_rest, cancellationToken);

		public Task<T> RunGraphQLAsync<T>(
			string query,
			JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
		public void Dispose()
		{
			_transport.Dispose();
			_httpClient.Dispose();
		}

		private sealed class StubHttpMessageHandler(FakeGitHubApiClient owner) : HttpMessageHandler
		{
			protected override async Task<HttpResponseMessage> SendAsync(
				HttpRequestMessage request,
				CancellationToken cancellationToken)
			{
				owner.RequestMethod = request.Method;
				owner.RequestUri = request.RequestUri?.PathAndQuery.TrimStart('/') ?? string.Empty;
				owner.RequestBody = request.Content is null
					? string.Empty
					: await request.Content.ReadAsStringAsync(cancellationToken);
				owner.AcceptsGitHubJson = request.Headers.Accept.Any(
					value => value.MediaType == "application/vnd.github+json");
				owner.ApiVersion = request.Headers.GetValues("X-GitHub-Api-Version").Single();

				return new HttpResponseMessage(owner.StatusCode)
				{
					Content = new StringContent(owner.ResponseBody),
					RequestMessage = request,
				};
			}
		}
	}
}
