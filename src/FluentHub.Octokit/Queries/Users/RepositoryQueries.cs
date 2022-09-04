namespace FluentHub.Octokit.Queries.Users
{
    public class RepositoryQueries
    {
        public async Task<List<Repository>> GetAllAsync(string login)
        {
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
                .Repositories(first: 30)
                .Nodes
                .Select(x => new Repository
                {
                    Name = x.Name,
                    Description = x.Description,
                    StargazerCount = x.StargazerCount,
                    ForkCount = x.ForkCount,
                    IsFork = x.IsFork,
                    IsPrivate = x.IsPrivate,
                    IsInOrganization = x.IsInOrganization,
                    ViewerHasStarred = x.ViewerHasStarred,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

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
                        AvatarUrl = owner.AvatarUrl(100),
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
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
