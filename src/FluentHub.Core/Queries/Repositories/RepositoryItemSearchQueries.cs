using System.IO;
using System.Net;
using System.Net.Http.Headers;
using GraphQL;
using FluentHub.Core.Clients;
using Newtonsoft.Json.Linq;

namespace FluentHub.Core.Queries.Repositories
{
	internal sealed class RepositoryItemSearchQueries
	{
		private const string IssueSearchQuery = """
			query($query: String!, $first: Int!, $after: String) {
			  search(query: $query, type: ISSUE, first: $first, after: $after) {
			    nodes {
			      ... on Issue {
			        author { avatarUrl(size: 500) login }
			        closed
			        comments { totalCount }
			        labels(first: 10) { nodes { color description name } }
			        number
			        repository { name owner { avatarUrl(size: 500) login } }
			        title
			        updatedAt
			      }
			    }
			    pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			  }
			}
			""";

		private const string PullRequestSearchQuery = """
			query($query: String!, $first: Int!, $after: String) {
			  search(query: $query, type: ISSUE, first: $first, after: $after) {
			    nodes {
			      ... on PullRequest {
			        author { avatarUrl(size: 500) login }
			        closed
			        comments { totalCount }
			        isDraft
			        labels(first: 10) { nodes { color description name } }
			        merged
			        number
			        repository { name owner { avatarUrl(size: 500) login } }
			        title
			        updatedAt
			      }
			    }
			    pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			  }
			}
			""";

		private const string AuthorSearchQuery = """
			query($query: String!) {
			  search(query: $query, type: ISSUE, first: 100) {
			    nodes {
			      ... on Issue { author { login } }
			      ... on PullRequest { author { login } }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public RepositoryItemSearchQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<PageResult<Issue>> GetIssuePageAsync(
			string owner,
			string name,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken)
		{
			var connection = await SearchAsync<IssueSearchNode>(
				IssueSearchQuery,
				RepositoryItemSearchQueryBuilder.Build(owner, name, false, filters),
				page,
				cancellationToken);

			return new PageResult<Issue>(
				connection.Nodes.Where(node => node is not null).Select(node => MapIssue(node!)).ToList(),
				connection.PageInfo);
		}

		public async Task<PageResult<PullRequest>> GetPullRequestPageAsync(
			string owner,
			string name,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken)
		{
			var connection = await SearchAsync<PullRequestSearchNode>(
				PullRequestSearchQuery,
				RepositoryItemSearchQueryBuilder.Build(owner, name, true, filters),
				page,
				cancellationToken);

			return new PageResult<PullRequest>(
				connection.Nodes.Where(node => node is not null).Select(node => MapPullRequest(node!)).ToList(),
				connection.PageInfo);
		}

		public async Task<IReadOnlyList<string>> GetAuthorLoginsAsync(
			string owner,
			string name,
			bool isPullRequest,
			CancellationToken cancellationToken)
		{
			var filters = new RepositoryItemListFilters
			{
				State = RepositoryItemStateFilter.All,
				Sort = RepositoryItemSort.BestMatch,
			};
			var request = new GraphQLRequest
			{
				Query = AuthorSearchQuery,
				Variables = new
				{
					query = RepositoryItemSearchQueryBuilder.Build(owner, name, isPullRequest, filters),
				},
			};
			var response = await _gitHub.SendGraphQLAsync<SearchResponse<AuthorSearchNode>>(
				request,
				cancellationToken);

			ThrowIfErrors(response.Errors);

			return response.Data?.Search?.Nodes
				.Where(node => !string.IsNullOrWhiteSpace(node?.Author?.Login))
				.Select(node => node!.Author!.Login)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(login => login, StringComparer.OrdinalIgnoreCase)
				.ToList() ?? [];
		}

		public async Task<IReadOnlyList<string>> GetIssueTypeNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken)
		{
			var endpoint = $"repos/{Uri.EscapeDataString(owner)}/{Uri.EscapeDataString(name)}/issue-types";
			using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
			request.Headers.Add("X-GitHub-Api-Version", "2026-03-10");

			using var response = await _gitHub.SendRestAsync(request, cancellationToken);
			if (response.StatusCode == HttpStatusCode.NotFound)
				return [];

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync(cancellationToken);
			return JArray.Parse(content)
				.Select(item => item["name"]?.ToString())
				.Where(type => !string.IsNullOrWhiteSpace(type))
				.Select(type => type!)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(type => type, StringComparer.OrdinalIgnoreCase)
				.ToList();
		}

		private async Task<SearchConnection<TNode>> SearchAsync<TNode>(
			string query,
			string searchText,
			PageRequest page,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(page);

			if (page.First is not int count)
				throw new NotSupportedException("Repository item search only supports forward pagination.");

			var request = new GraphQLRequest
			{
				Query = query,
				Variables = new
				{
					query = searchText,
					first = Math.Min(count, 100),
					after = page.After,
				},
			};
			var response = await _gitHub.SendGraphQLAsync<SearchResponse<TNode>>(request, cancellationToken);

			ThrowIfErrors(response.Errors);
			return response.Data?.Search
				?? throw new InvalidDataException("GitHub returned an incomplete repository item search response.");
		}

		private static void ThrowIfErrors(GraphQLError[]? errors)
		{
			if (errors is not { Length: > 0 })
				return;

			throw new InvalidOperationException(string.Join("; ", errors.Select(error => error.Message)));
		}

		private static Issue MapIssue(IssueSearchNode node)
			=> new()
			{
				Author = MapActor(node.Author),
				Closed = node.Closed,
				Comments = new IssueCommentConnection { TotalCount = node.Comments.TotalCount },
				Labels = MapLabels(node.Labels),
				Number = node.Number,
				Repository = MapRepository(node.Repository),
				Title = node.Title,
				UpdatedAt = node.UpdatedAt,
				UpdatedAtHumanized = node.UpdatedAt.ToRelativeTime(),
			};

		private static PullRequest MapPullRequest(PullRequestSearchNode node)
			=> new()
			{
				Author = MapActor(node.Author),
				Closed = node.Closed,
				Comments = new IssueCommentConnection { TotalCount = node.Comments.TotalCount },
				IsDraft = node.IsDraft,
				Labels = MapLabels(node.Labels),
				Merged = node.Merged,
				Number = node.Number,
				Repository = MapRepository(node.Repository),
				Title = node.Title,
				UpdatedAt = node.UpdatedAt,
				UpdatedAtHumanized = node.UpdatedAt.ToRelativeTime(),
			};

		private static Actor? MapActor(ActorNode? actor)
			=> actor is null
				? null
				: new Actor
				{
					AvatarUrl = actor.AvatarUrl ?? string.Empty,
					Login = actor.Login,
				};

		private static LabelConnection MapLabels(LabelConnectionNode labels)
			=> new()
			{
				Nodes = labels.Nodes
					.Where(label => label is not null)
					.Select(label => (Label?)new Label
					{
						Color = label!.Color,
						Description = label.Description,
						Name = label.Name,
					})
					.ToList(),
			};

		private static Repository MapRepository(RepositoryNode repository)
			=> new()
			{
				Name = repository.Name,
				Owner = new RepositoryOwner
				{
					AvatarUrl = repository.Owner.AvatarUrl,
					Login = repository.Owner.Login,
				},
			};

		private sealed class SearchResponse<TNode>
		{
			public SearchConnection<TNode>? Search { get; set; }
		}

		private sealed class SearchConnection<TNode>
		{
			public List<TNode?> Nodes { get; set; } = [];

			public PageInfo PageInfo { get; set; } = new();
		}

		private sealed class AuthorSearchNode
		{
			public ActorNode? Author { get; set; }
		}

		private class IssueSearchNode
		{
			public ActorNode? Author { get; set; }

			public bool Closed { get; set; }

			public CountNode Comments { get; set; } = new();

			public LabelConnectionNode Labels { get; set; } = new();

			public int Number { get; set; }

			public RepositoryNode Repository { get; set; } = new();

			public string Title { get; set; } = string.Empty;

			public DateTimeOffset UpdatedAt { get; set; }
		}

		private sealed class PullRequestSearchNode : IssueSearchNode
		{
			public bool IsDraft { get; set; }

			public bool Merged { get; set; }
		}

		private sealed class ActorNode
		{
			public string? AvatarUrl { get; set; }

			public string Login { get; set; } = string.Empty;
		}

		private sealed class CountNode
		{
			public int TotalCount { get; set; }
		}

		private sealed class LabelConnectionNode
		{
			public List<LabelNode?> Nodes { get; set; } = [];
		}

		private sealed class LabelNode
		{
			public string Color { get; set; } = string.Empty;

			public string? Description { get; set; }

			public string Name { get; set; } = string.Empty;
		}

		private sealed class RepositoryNode
		{
			public string Name { get; set; } = string.Empty;

			public RepositoryOwnerNode Owner { get; set; } = new();
		}

		private sealed class RepositoryOwnerNode
		{
			public string AvatarUrl { get; set; } = string.Empty;

			public string Login { get; set; } = string.Empty;
		}
	}
}
