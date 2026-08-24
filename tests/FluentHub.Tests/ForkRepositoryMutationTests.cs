using FluentHub.Core.Clients;
using FluentHub.Core.Contracts;
using FluentHub.Core.Mutations;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.GraphQL;
using System.Net;
using System.Text.Json;

namespace FluentHub.Tests;

[TestClass]
public sealed class ForkRepositoryMutationTests
{
	[TestMethod]
	public async Task CreateForkSendsCustomDestinationNameAndBranchSelection()
	{
		var api = new FakeGitHubApiClient();
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
		Assert.AreEqual("2022-11-28", api.ApiVersion);

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
		var api = new FakeGitHubApiClient();
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
		var api = new FakeGitHubApiClient
		{
			ResponseBody = "{\"message\":\"Validation Failed\",\"errors\":[{\"field\":\"name\",\"code\":\"already_exists\"}]}",
			StatusCode = HttpStatusCode.UnprocessableEntity,
		};
		var mutation = new ForkRepositoryMutation(api);

		var exception = await Assert.ThrowsExactlyAsync<HttpRequestException>(() =>
			mutation.ExecuteAsync(new CreateForkRequest
			{
				DestinationOwner = new ForkOwner { Login = "viewer" },
				RepositoryName = "source-repo",
				SourceName = "source-repo",
				SourceOwner = "source-owner",
			}));

		StringAssert.Contains(exception.Message, "name: already_exists");
	}

	private sealed class FakeGitHubApiClient : IGitHubApiClient
	{
		public string ApiVersion { get; private set; } = string.Empty;
		public bool AcceptsGitHubJson { get; private set; }
		public string RequestBody { get; private set; } = string.Empty;
		public HttpMethod? RequestMethod { get; private set; }
		public string RequestUri { get; private set; } = string.Empty;
		public string ResponseBody { get; init; } =
			"{\"name\":\"renamed-fork\",\"full_name\":\"destination-org/renamed-fork\",\"owner\":{\"login\":\"destination-org\"}}";
		public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.Accepted;

		public Task<T> RunRestAsync<T>(
			Func<global::Octokit.IGitHubClient, Task<T>> operation,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();

		public Task<T> RunGraphQLAsync<T>(
			ICompiledQuery<T> query,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();

		public Task<GraphQLResponse<T>> SendGraphQLAsync<T>(
			GraphQLRequest request,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();

		public async Task<HttpResponseMessage> SendRestAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken = default)
		{
			RequestMethod = request.Method;
			RequestUri = request.RequestUri?.ToString() ?? string.Empty;
			RequestBody = await request.Content!.ReadAsStringAsync(cancellationToken);
			AcceptsGitHubJson = request.Headers.Accept.Any(value => value.MediaType == "application/vnd.github+json");
			ApiVersion = request.Headers.GetValues("X-GitHub-Api-Version").Single();

			return new HttpResponseMessage(StatusCode)
			{
				Content = new StringContent(ResponseBody),
			};
		}
	}
}
