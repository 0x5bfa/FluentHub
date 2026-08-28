using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class StarredRepoQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public StarredRepoQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Repository>> GetPageAsync(
			string login,
			PageRequest page,
			OctokitGraphQLModel.StarOrder? orderBy = null,
			bool? ownedByViewer = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[]
				{
					OctokitGraphQLModel.IssueState.Open
				});

			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[]
				{
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.User(login)
				.StarredRepositories(
					page.First,
					page.After,
					page.Last,
					page.Before,
					orderBy,
					ownedByViewer)
				.Select(connection => new RepositoryConnection
				{
					Edges = connection.Edges.Select(edge => (RepositoryEdge?)new RepositoryEdge
					{
						Node = edge.Node.Select(x => new Repository
						{
							Name = x.Name,
							Description = x.Description,
							StargazerCount = x.StargazerCount,
							ForkCount = x.ForkCount,
							HasSponsorshipsEnabled = x.HasSponsorshipsEnabled,
							Id = x.Id,
							IsArchived = x.IsArchived,
							IsFork = x.IsFork,
							IsInOrganization = x.IsInOrganization,
							IsMirror = x.IsMirror,
							IsPrivate = x.IsPrivate,
							IsTemplate = x.IsTemplate,
							PushedAt = x.PushedAt,
							ViewerHasStarred = x.ViewerHasStarred,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),

							LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
							{
								Name = licenseInfo.Name,
							})
							.SingleOrDefault(),

							Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
							{
								TotalCount = issues.TotalCount
							})
							.Single(),

							PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
							{
								TotalCount = issues.TotalCount
							})
							.Single(),

							Owner = x.Owner.Select(owner => new RepositoryOwner
							{
								AvatarUrl = owner.AvatarUrl(500),
								Id = owner.Id,
								Login = owner.Login,
							})
							.Single(),

							PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
							{
								Name = y.Name,
								Color = y.Color,
							})
							.SingleOrDefault(),
						}).Single()
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Repository>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<IReadOnlyList<Repository>> GetAllAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			var repositories = new List<Repository>();
			PageRequest? page = PageRequest.Forward(100);
			var order = new OctokitGraphQLModel.StarOrder
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.StarOrderField.StarredAt,
			};

			do
			{
				var result = await GetPageAsync(login, page, order, cancellationToken: cancellationToken);
				repositories.AddRange(result.Items);
				page = result.PageInfo.HasNextPage
					&& !string.IsNullOrEmpty(result.PageInfo.EndCursor)
					? PageRequest.Forward(100, result.PageInfo.EndCursor)
					: null;
			}
			while (page is not null);

			return repositories;
		}

		public async Task<IReadOnlyList<string>> GetLanguagesAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);

			var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string? cursor = null;
			do
			{
				var query = new Query()
					.User(login)
					.StarredRepositories(first: 100, after: cursor)
					.Select(connection => new StarredRepositoryConnection
					{
						Nodes = connection.Nodes.Select(node => (Repository?)new Repository
						{
							PrimaryLanguage = node.PrimaryLanguage.Select(language => new Language
							{
								Name = language.Name,
							})
							.SingleOrDefault(),
						}).ToList(),
						PageInfo = new PageInfo
						{
							EndCursor = connection.PageInfo.EndCursor,
							HasNextPage = connection.PageInfo.HasNextPage,
						},
					})
					.Compile();
				var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);
				foreach (var language in response.Nodes?
					.Select(repository => repository?.PrimaryLanguage?.Name)
					.Where(name => !string.IsNullOrWhiteSpace(name)) ?? [])
				{
					languages.Add(language!);
				}

				cursor = response.PageInfo.HasNextPage
					? response.PageInfo.EndCursor
					: null;
			}
			while (cursor is not null);

			return languages.OrderBy(language => language, StringComparer.OrdinalIgnoreCase).ToList();
		}
	}
}
