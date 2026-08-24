using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class RepositoryQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public RepositoryQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Repository>> GetPageAsync(
			string login,
			PageRequest page,
			IEnumerable<OctokitGraphQLModel.RepositoryAffiliation?>? affiliations = null,
			bool? isArchived = null,
			bool? isFork = null,
			bool? isLocked = null,
			OctokitGraphQLModel.RepositoryOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.RepositoryAffiliation?>? ownerAffiliations = null,
			OctokitGraphQLModel.RepositoryPrivacy? privacy = null,
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
				.Repositories(
					first: page.First,
					after: page.After,
					last: page.Last,
					before: page.Before,
					affiliations: affiliations is null ? null! : new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.RepositoryAffiliation?>>(affiliations),
					isArchived: isArchived,
					isFork: isFork,
					isLocked: isLocked,
					orderBy: orderBy,
					ownerAffiliations: ownerAffiliations is null ? null! : new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.RepositoryAffiliation?>>(ownerAffiliations),
					privacy: privacy)
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
							IsPrivate = x.IsPrivate,
							IsInOrganization = x.IsInOrganization,
							IsMirror = x.IsMirror,
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

					PageInfo = new PageInfo()
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

		public Task<IReadOnlyList<Repository>> SearchAllAsync(
			string login,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken = default)
			=> new UserRepositorySearchQueries(_gitHub).GetAllAsync(login, filters, cancellationToken);

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
					.Repositories(first: 100, after: cursor)
					.Select(connection => new RepositoryConnection
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
