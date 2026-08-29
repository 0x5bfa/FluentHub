using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Queries.Users;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Organizations
{
	public class RepositoryQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public RepositoryQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<List<Repository>> GetAllAsync(
			string organization,
			CancellationToken cancellationToken = default)
			=> (await GetPageAsync(
				organization,
				PageRequest.Forward(30),
				cancellationToken: cancellationToken)).Items.ToList();

		public async Task<PageResult<Repository>> GetPageAsync(
			string organization,
			PageRequest page,
			bool? isArchived = null,
			bool? isFork = null,
			bool? isLocked = null,
			OctokitGraphQLModel.RepositoryOrder? orderBy = null,
			OctokitGraphQLModel.RepositoryPrivacy? privacy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(organization);
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
				.Organization(organization)
				.Repositories(
					first: page.First,
					after: page.After,
					last: page.Last,
					before: page.Before,
					isArchived: isArchived,
					isFork: isFork,
					isLocked: isLocked,
					orderBy: orderBy,
					privacy: privacy)
				.Select(connection => new RepositoryConnection
				{
					Edges = connection.Edges.Select(edge => (RepositoryEdge?)new RepositoryEdge
					{
						Node = edge.Node.Select(repository => new Repository
						{
							Name = repository.Name,
							Description = repository.Description,
							StargazerCount = repository.StargazerCount,
							ForkCount = repository.ForkCount,
							HasSponsorshipsEnabled = repository.HasSponsorshipsEnabled,
							Id = repository.Id,
							IsArchived = repository.IsArchived,
							IsFork = repository.IsFork,
							IsPrivate = repository.IsPrivate,
							IsInOrganization = repository.IsInOrganization,
							IsMirror = repository.IsMirror,
							IsTemplate = repository.IsTemplate,
							PushedAt = repository.PushedAt,
							ViewerHasStarred = repository.ViewerHasStarred,
							UpdatedAt = repository.UpdatedAt,
							UpdatedAtHumanized = repository.UpdatedAt.ToRelativeTime(),
							LicenseInfo = repository.LicenseInfo.Select(licenseInfo => new License
							{
								Name = licenseInfo.Name,
							})
							.SingleOrDefault(),
							Issues = repository.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
							{
								TotalCount = issues.TotalCount,
							})
							.Single(),
							PullRequests = repository.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(pullRequests => new PullRequestConnection
							{
								TotalCount = pullRequests.TotalCount,
							})
							.Single(),
							Owner = repository.Owner.Select(owner => new RepositoryOwner
							{
								AvatarUrl = owner.AvatarUrl(500),
								Id = owner.Id,
								Login = owner.Login,
							})
							.Single(),
							PrimaryLanguage = repository.PrimaryLanguage.Select(language => new Language
							{
								Name = language.Name,
								Color = language.Color,
							})
							.SingleOrDefault(),
						}).Single(),
					}).ToList(),
					PageInfo = new PageInfo
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
					.Where(edge => edge?.Node is not null)
					.Select(edge => edge!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public Task<IReadOnlyList<Repository>> SearchAllAsync(
			string organization,
			UserRepositoryListFilters filters,
			CancellationToken cancellationToken = default)
			=> new UserRepositorySearchQueries(_gitHub)
				.GetOrganizationAllAsync(organization, filters, cancellationToken);

		public async Task<IReadOnlyList<string>> GetLanguagesAsync(
			string organization,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(organization);

			var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string? cursor = null;
			do
			{
				var query = new Query()
					.Organization(organization)
					.Repositories(first: 100, after: cursor)
					.Select(connection => new RepositoryConnection
					{
						Nodes = connection.Nodes.Select(repository => (Repository?)new Repository
						{
							PrimaryLanguage = repository.PrimaryLanguage.Select(language => new Language
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
