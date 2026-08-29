// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	public partial class PullRequestQueries
	{
		private const string ReactionFields = """
			reactionGroups { content viewerHasReacted reactors { totalCount } }
		""";

		[GeneratedGraphQLOperation<GraphQLResult<Repository>>]
		private const string ItemQuery = """
			query PullRequest($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    pullRequest(number: $number) {
			      additions authorAssociation baseRefName body changedFiles closed createdAt deletions
			      headRefName headRefOid id isDraft lastEditedAt mergeable merged number state title updatedAt url
			      viewerCanClose: viewerCanUpdate viewerCanMergeAsAdmin viewerCanReact
			      viewerCanReopen: viewerCanUpdate viewerCanSubscribe viewerCanUpdate viewerDidAuthor viewerSubscription
			      author { avatarUrl(size: 500) login }
			      assignees(first: 6) { nodes { avatarUrl(size: 500) login } }
			      comments { totalCount }
			      commits(last: 1) { totalCount nodes { commit { statusCheckRollup { state } } } }
			      headRepository { name owner { avatarUrl(size: 500) login } }
			      labels(first: 10) { nodes { color description name } }
			      latestReviews(first: 15) { nodes { author { avatarUrl(size: 500) login } } }
			      milestone { title progressPercentage }
			      participants(first: 6) { nodes { avatarUrl(size: 500) login } }
			""" + ReactionFields + """
			      repository { name viewerPermission owner { avatarUrl(size: 500) id login } }
			      reviewRequests(first: 15) { nodes { requestedReviewer { ... on User { avatarUrl(size: 500) login } } } }
			      reviews(last: 1) { nodes { state } }
			    }
			  }
			}
			""";

		[GeneratedGraphQLOperation<GraphQLResult<RepositoryBodyResult>>]
		private const string BodyQuery = """
			query PullRequestBody($owner: String!, $name: String!, $number: Int!) {
			  result: repository(owner: $owner, name: $name) {
			    pullRequest(number: $number) {
			      authorAssociation body createdAt id lastEditedAt updatedAt url
			      viewerCanReact viewerCanUpdate viewerDidAuthor
			      author { login avatarUrl(size: 500) }
			""" + ReactionFields + """
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public PullRequestQueries(IGitHubApiClient gitHub) => _gitHub = gitHub;

		public Task<PageResult<PullRequest>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			RepositoryItemListFilters? filters = null,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetPullRequestPageAsync(owner, name, page, filters ?? new(), cancellationToken);

		public Task<IReadOnlyList<string>> GetAuthorLoginsAsync(
			string owner,
			string name,
			CancellationToken cancellationToken = default)
			=> new RepositoryItemSearchQueries(_gitHub).GetAuthorLoginsAsync(owner, name, true, cancellationToken);

		public async Task<PullRequest> GetAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				ItemQueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepository,
				writer => WriteVariables(writer, owner, name, number),
				cancellationToken);
			var pullRequest = response.Result?.PullRequest
				?? throw new InvalidDataException($"GitHub pull request '{owner}/{name}#{number}' was not found.");
			pullRequest.CreatedAtHumanized = pullRequest.CreatedAt.ToRelativeTime();
			pullRequest.UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime();
			foreach (var request in pullRequest.ReviewRequests?.Nodes?.Where(item => item?.RequestedReviewer is not null).Select(item => item!.RequestedReviewer!) ?? [])
			{
				if (!string.IsNullOrWhiteSpace(request.Login))
					request.User = new User { Login = request.Login, AvatarUrl = request.AvatarUrl ?? string.Empty };
			}
			return pullRequest;
		}

		public async Task<IssueComment> GetBodyAsync(
			string owner,
			string name,
			int number,
			CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunGraphQLAsync(
				BodyQueryOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultRepositoryBodyResult,
				writer => WriteVariables(writer, owner, name, number),
				cancellationToken);
			var body = response.Result?.PullRequest
				?? throw new InvalidDataException($"GitHub pull request '{owner}/{name}#{number}' was not found.");
			body.CreatedAtHumanized = body.CreatedAt.ToRelativeTime();
			body.UpdatedAtHumanized = body.UpdatedAt.ToRelativeTime();
			return body;
		}

		private static void WriteVariables(System.Text.Json.Utf8JsonWriter writer, string owner, string name, int number)
		{
			writer.WriteString("owner", owner);
			writer.WriteString("name", name);
			writer.WriteNumber("number", number);
		}
	}
}
