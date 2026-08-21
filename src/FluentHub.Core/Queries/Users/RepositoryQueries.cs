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
							Id = x.Id,
							IsFork = x.IsFork,
							IsPrivate = x.IsPrivate,
							IsInOrganization = x.IsInOrganization,
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
	}
}
