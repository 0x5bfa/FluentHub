namespace FluentHub.Octokit.Queries.Organizations
{
    public class RepositoryQueries
    {
        public async Task<List<Repository>> GetAllAsync(string org)
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

            #region query
            var query = new Query()
                .Organization(org)
                .Repositories(first: 30)
                .Nodes
                .Select(x => new Repository
                {
                    Name = x.Name,
                    Description = x.Description,
                    LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
                    {
                        Name = licenseInfo.Name,
                    })
                    .SingleOrDefault(),

                    StargazerCount = x.StargazerCount,
                    ForkCount = x.ForkCount,

                    Issues = new()
                    {
                        TotalCount = x.Issues(null, null, null, null, null, null, null, issueState).TotalCount
                    },

                    PullRequests = new()
                    {
                        TotalCount = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).TotalCount
                    },

                    IsFork = x.IsFork,
                    IsInOrganization = x.IsInOrganization,
                    ViewerHasStarred = x.ViewerHasStarred,

                    UpdatedAt = x.UpdatedAt,

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
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
