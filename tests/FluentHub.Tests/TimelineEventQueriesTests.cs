using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed class TimelineEventQueriesTests
{
	[TestMethod]
	public async Task IssueTimelineRestoresUnionMembers()
	{
		var api = new JsonGitHubApiClient("""
			{
			  "result": {
			    "issue": {
			      "timelineItems": {
			        "nodes": [
			          {
			            "__typename": "AssignedEvent",
			            "createdAt": "2026-08-01T00:00:00Z",
			            "id": "assigned-id",
			            "actor": { "avatarUrl": "https://example.test/avatar", "login": "octocat" },
			            "assignee": { "__typename": "User", "login": "hubot" }
			          },
			          {
			            "__typename": "ConnectedEvent",
			            "createdAt": "2026-08-02T00:00:00Z",
			            "id": "connected-id",
			            "source": { "__typename": "Issue", "number": 1, "title": "Source issue" },
			            "subject": { "__typename": "PullRequest", "number": 2, "title": "Target pull request" }
			          }
			        ]
			      }
			    }
			  }
			}
			""");

		var events = await new IssueEventQueries(api).GetAllAsync("owner", "repository", 1);

		Assert.HasCount(2, events);
		var assigned = (AssignedEvent)events[0];
		Assert.AreEqual("octocat", assigned.Actor?.Login);
		Assert.AreEqual("hubot", assigned.Assignee?.User?.Login);
		Assert.IsFalse(string.IsNullOrWhiteSpace(assigned.CreatedAtHumanized));
		var connected = (ConnectedEvent)events[1];
		Assert.AreEqual("Source issue", connected.Source.Issue?.Title);
		Assert.AreEqual("Target pull request", connected.Subject.PullRequest?.Title);
	}

	[TestMethod]
	public async Task PullRequestTimelineReadsCommitAndRequestedReviewer()
	{
		var api = new JsonGitHubApiClient("""
			{
			  "result": {
			    "pullRequest": {
			      "timelineItems": {
			        "nodes": [
			          {
			            "__typename": "PullRequestCommit",
			            "id": "commit-id",
			            "commit": {
			              "message": "Native AOT",
			              "author": { "avatarUrl": "https://example.test/avatar", "user": { "login": "octocat" } }
			            }
			          },
			          {
			            "__typename": "ReviewRequestedEvent",
			            "createdAt": "2026-08-03T00:00:00Z",
			            "requestedReviewer": {
			              "__typename": "User",
			              "avatarUrl": "https://example.test/reviewer",
			              "login": "reviewer"
			            }
			          }
			        ]
			      }
			    }
			  }
			}
			""");

		var events = await new PullRequestEventQueries(api).GetAllAsync("owner", "repository", 1);

		Assert.HasCount(2, events);
		var commit = (PullRequestCommit)events[0];
		Assert.AreEqual("Native AOT", commit.Commit.Message);
		Assert.AreEqual("octocat", commit.Commit.Author?.User?.Login);
		var reviewRequested = (ReviewRequestedEvent)events[1];
		Assert.AreEqual("reviewer", reviewRequested.RequestedReviewer?.User?.Login);
	}

	private sealed class JsonGitHubApiClient(string responseJson) : IGitHubApiClient
	{
		public Task<T> RunGraphQLAsync<T>(
			string query,
			JsonTypeInfo<T> dataTypeInfo,
			Action<Utf8JsonWriter>? writeVariables = null,
			CancellationToken cancellationToken = default)
			=> Task.FromResult(JsonSerializer.Deserialize(responseJson, dataTypeInfo)!);

		public Task<T> RunRestAsync<T>(
			Func<global::Octokit.Rest.GitHubRestClient, CancellationToken, Task<T>> operation,
			CancellationToken cancellationToken = default)
			=> throw new NotSupportedException();
	}
}
