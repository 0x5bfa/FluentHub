using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories
{
	internal sealed partial class RepositoryItemSearchQueries
	{
		private const string IssueSearchQuery = """
			query($query: String!, $first: Int!, $after: String) {
			  search(query: $query, type: ISSUE, first: $first, after: $after) {
			    nodes {
			      ... on Issue {
			        author { avatarUrl(size: 500) login }
			        assignees(first: 10) { nodes { login } }
			        closed
			        comments { totalCount }
			        issueType { name }
			        labels(first: 10) { nodes { color description name } }
			        milestone { title }
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
			        assignees(first: 10) { nodes { login } }
			        closed
			        comments { totalCount }
			        isDraft
			        labels(first: 10) { nodes { color description name } }
			        milestone { title }
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

		public async Task<PageResult<Issue>> GetUserIssuePageAsync(
			string login,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken)
		{
			var connection = await SearchAsync<IssueSearchNode>(
				IssueSearchQuery,
				RepositoryItemSearchQueryBuilder.BuildForAuthor(login, false, filters),
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

		public async Task<PageResult<PullRequest>> GetUserPullRequestPageAsync(
			string login,
			PageRequest page,
			RepositoryItemListFilters filters,
			CancellationToken cancellationToken)
		{
			var connection = await SearchAsync<PullRequestSearchNode>(
				PullRequestSearchQuery,
				RepositoryItemSearchQueryBuilder.BuildForAuthor(login, true, filters),
				page,
				cancellationToken);

			return new PageResult<PullRequest>(
				connection.Nodes.Where(node => node is not null).Select(node => MapPullRequest(node!)).ToList(),
				connection.PageInfo);
		}

		public async Task<RepositoryItemFilterOptions> GetUserFilterOptionsAsync(
			string login,
			bool isPullRequest,
			CancellationToken cancellationToken)
		{
			var filters = new RepositoryItemListFilters
			{
				State = RepositoryItemStateFilter.All,
				Sort = RepositoryItemSort.BestMatch,
			};
			if (isPullRequest)
			{
				var pullRequests = await SearchAsync<PullRequestSearchNode>(
					PullRequestSearchQuery,
					RepositoryItemSearchQueryBuilder.BuildForAuthor(login, true, filters),
					PageRequest.Forward(100),
					cancellationToken);
				return CreateFilterOptions(pullRequests.Nodes);
			}

			var issues = await SearchAsync<IssueSearchNode>(
				IssueSearchQuery,
				RepositoryItemSearchQueryBuilder.BuildForAuthor(login, false, filters),
				PageRequest.Forward(100),
				cancellationToken);
			return CreateFilterOptions(issues.Nodes);
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
			var response = await _gitHub.RunGraphQLAsync(
				AuthorSearchQuery,
				GetJsonTypeInfo<SearchResponse<AuthorSearchNode>>(),
				writer => writer.WriteString(
					"query",
					RepositoryItemSearchQueryBuilder.Build(owner, name, isPullRequest, filters)),
				cancellationToken);

			return response.Search?.Nodes
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
			try
			{
				var response = await _gitHub.RunRestAsync(
					(client, token) => client.Repositories.GetIssueTypesAsync(owner, name, token),
					cancellationToken);

				return response
				.Select(item => item.Name)
				.Where(type => !string.IsNullOrWhiteSpace(type))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(type => type, StringComparer.OrdinalIgnoreCase)
				.ToList();
			}
			catch (global::Octokit.Transport.GitHubApiException exception)
				when (exception.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return [];
			}
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

			var response = await _gitHub.RunGraphQLAsync(
				query,
				GetJsonTypeInfo<SearchResponse<TNode>>(),
				writer =>
				{
					writer.WriteString("query", searchText);
					writer.WriteNumber("first", Math.Min(count, 100));
					GraphQLInputWriter.WriteOptionalString(writer, "after", page.After);
				},
				cancellationToken);

			return response.Search
				?? throw new InvalidDataException("GitHub returned an incomplete repository item search response.");
		}

		private static JsonTypeInfo<T> GetJsonTypeInfo<T>()
		{
			return (JsonTypeInfo<T>)(RepositoryItemSearchJsonContext.Default.GetTypeInfo(typeof(T))
				?? throw new InvalidOperationException($"No JSON metadata is registered for {typeof(T)}."));
		}

		private static RepositoryItemFilterOptions CreateFilterOptions<TNode>(IEnumerable<TNode?> nodes)
			where TNode : IssueSearchNode
		{
			var items = nodes.Where(node => node is not null).Select(node => node!).ToList();
			return new RepositoryItemFilterOptions
			{
				Labels = SortDistinct(items.SelectMany(item => item.Labels.Nodes).Select(label => label?.Name)),
				IssueTypes = SortDistinct(items.Select(item => item.IssueType?.Name)),
				Assignees = SortDistinct(items.SelectMany(item => item.Assignees.Nodes).Select(user => user?.Login)),
				Milestones = SortDistinct(items.Select(item => item.Milestone?.Title)),
			};
		}

		private static IReadOnlyList<string> SortDistinct(IEnumerable<string?> values)
			=> values
				.Where(value => !string.IsNullOrWhiteSpace(value))
				.Select(value => value!)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
				.ToList();

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

			public UserConnectionNode Assignees { get; set; } = new();

			public bool Closed { get; set; }

			public CountNode Comments { get; set; } = new();

			public IssueTypeNode? IssueType { get; set; }

			public LabelConnectionNode Labels { get; set; } = new();

			public MilestoneNode? Milestone { get; set; }

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

		private sealed class UserConnectionNode
		{
			public List<UserNode?> Nodes { get; set; } = [];
		}

		private sealed class UserNode
		{
			public string Login { get; set; } = string.Empty;
		}

		private sealed class IssueTypeNode
		{
			public string Name { get; set; } = string.Empty;
		}

		private sealed class MilestoneNode
		{
			public string Title { get; set; } = string.Empty;
		}

		[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
		[JsonSerializable(typeof(SearchResponse<AuthorSearchNode>))]
		[JsonSerializable(typeof(SearchResponse<IssueSearchNode>))]
		[JsonSerializable(typeof(SearchResponse<PullRequestSearchNode>))]
		private sealed partial class RepositoryItemSearchJsonContext : JsonSerializerContext;
	}
}
