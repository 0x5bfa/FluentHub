using System.IO;
using GraphQL;
using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	internal sealed class UserRepositorySearchQueries
	{
		private const string RepositorySearchQuery = """
			query($query: String!, $first: Int!, $after: String) {
			  search(query: $query, type: REPOSITORY, first: $first, after: $after) {
			    nodes {
			      ... on Repository {
			        id
			        name
			        description
			        forkCount
			        hasSponsorshipsEnabled
			        isArchived
			        isFork
			        isMirror
			        isPrivate
			        isTemplate
			        pushedAt
			        stargazerCount
			        updatedAt
			        viewerHasStarred
			        issues(states: OPEN) { totalCount }
			        licenseInfo { name }
			        owner { avatarUrl(size: 500) id login }
			        primaryLanguage { color name }
			        pullRequests(states: OPEN) { totalCount }
			      }
			    }
			    pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public UserRepositorySearchQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<IReadOnlyList<Repository>> GetAllAsync(
			string login,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken)
			=> await GetAllForSearchAsync(
				UserRepositorySearchQueryBuilder.Build(login, filters),
				filters,
				cancellationToken);

		public async Task<IReadOnlyList<Repository>> GetOrganizationAllAsync(
			string organization,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken)
			=> await GetAllForSearchAsync(
				UserRepositorySearchQueryBuilder.BuildForOrganization(organization, filters),
				filters,
				cancellationToken);

		private async Task<IReadOnlyList<Repository>> GetAllForSearchAsync(
			string searchText,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken)
		{
			var repositories = new List<Repository>();
			string? cursor = null;

			do
			{
				var connection = await GetPageAsync(searchText, cursor, cancellationToken);
				repositories.AddRange(connection.Nodes
					.Where(node => node is not null)
					.Select(node => MapRepository(node!)));
				cursor = connection.PageInfo.HasNextPage
					? connection.PageInfo.EndCursor
					: null;
			}
			while (cursor is not null);

			return filters.Sort == UserRepositorySort.Name
				? repositories.OrderBy(repository => repository.Name, StringComparer.OrdinalIgnoreCase).ToList()
				: repositories;
		}

		private async Task<SearchConnection> GetPageAsync(
			string searchText,
			string? cursor,
			CancellationToken cancellationToken)
		{
			var request = new GraphQLRequest
			{
				Query = RepositorySearchQuery,
				Variables = new
				{
					query = searchText,
					first = 100,
					after = cursor,
				},
			};
			var response = await _gitHub.SendGraphQLAsync<SearchResponse>(request, cancellationToken);
			if (response.Errors is { Length: > 0 })
				throw new InvalidOperationException(string.Join("; ", response.Errors.Select(error => error.Message)));

			return response.Data?.Search
				?? throw new InvalidDataException("GitHub returned an incomplete repository search response.");
		}

		private static Repository MapRepository(RepositoryNode node)
			=> new()
			{
				Description = node.Description,
				ForkCount = node.ForkCount,
				HasSponsorshipsEnabled = node.HasSponsorshipsEnabled,
				Id = new ID(node.Id),
				IsArchived = node.IsArchived,
				IsFork = node.IsFork,
				IsMirror = node.IsMirror,
				IsPrivate = node.IsPrivate,
				IsTemplate = node.IsTemplate,
				Issues = new IssueConnection { TotalCount = node.Issues.TotalCount },
				LicenseInfo = node.LicenseInfo is null ? null : new License { Name = node.LicenseInfo.Name },
				Name = node.Name,
				Owner = new RepositoryOwner
				{
					AvatarUrl = node.Owner.AvatarUrl,
					Id = new ID(node.Owner.Id),
					Login = node.Owner.Login,
				},
				PrimaryLanguage = node.PrimaryLanguage is null
					? null
					: new Language
					{
						Color = node.PrimaryLanguage.Color,
						Name = node.PrimaryLanguage.Name,
					},
				PullRequests = new PullRequestConnection { TotalCount = node.PullRequests.TotalCount },
				PushedAt = node.PushedAt,
				StargazerCount = node.StargazerCount,
				UpdatedAt = node.UpdatedAt,
				UpdatedAtHumanized = node.UpdatedAt.ToRelativeTime(),
				ViewerHasStarred = node.ViewerHasStarred,
			};

		private sealed class SearchResponse
		{
			public SearchConnection? Search { get; set; }
		}

		private sealed class SearchConnection
		{
			public List<RepositoryNode?> Nodes { get; set; } = [];

			public PageInfo PageInfo { get; set; } = new();
		}

		private sealed class RepositoryNode
		{
			public string? Description { get; set; }

			public int ForkCount { get; set; }

			public bool HasSponsorshipsEnabled { get; set; }

			public string Id { get; set; } = string.Empty;

			public bool IsArchived { get; set; }

			public bool IsFork { get; set; }

			public bool IsMirror { get; set; }

			public bool IsPrivate { get; set; }

			public bool IsTemplate { get; set; }

			public CountNode Issues { get; set; } = new();

			public LicenseNode? LicenseInfo { get; set; }

			public string Name { get; set; } = string.Empty;

			public RepositoryOwnerNode Owner { get; set; } = new();

			public LanguageNode? PrimaryLanguage { get; set; }

			public CountNode PullRequests { get; set; } = new();

			public DateTimeOffset? PushedAt { get; set; }

			public int StargazerCount { get; set; }

			public DateTimeOffset UpdatedAt { get; set; }

			public bool ViewerHasStarred { get; set; }
		}

		private sealed class CountNode
		{
			public int TotalCount { get; set; }
		}

		private sealed class LanguageNode
		{
			public string? Color { get; set; }

			public string Name { get; set; } = string.Empty;
		}

		private sealed class LicenseNode
		{
			public string Name { get; set; } = string.Empty;
		}

		private sealed class RepositoryOwnerNode
		{
			public string AvatarUrl { get; set; } = string.Empty;

			public string Id { get; set; } = string.Empty;

			public string Login { get; set; } = string.Empty;
		}
	}
}
