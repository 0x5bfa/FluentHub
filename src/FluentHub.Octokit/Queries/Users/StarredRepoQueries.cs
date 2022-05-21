namespace FluentHub.Octokit.Queries.Users
{
    public class StarredRepoQueries
    {
        public StarredRepoQueries() => new App();

        public async Task<List<Repository>> GetAllAsync(string login)
        {
            OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState = new(new OctokitGraphQLModel.IssueState[] { OctokitGraphQLModel.IssueState.Open });
            OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState = new(new OctokitGraphQLModel.PullRequestState[] { OctokitGraphQLModel.PullRequestState.Open });

            #region query
            var query = new Query()
                    .User(login)
                    .StarredRepositories(first: 30)
                    .Nodes
                    .Select(x => new Repository
                    {
                        Name = x.Name,
                        Description = x.Description,
                        LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),

                        StargazerCount = x.StargazerCount,
                        ForkCount = x.ForkCount,
                        OpenIssueCount = x.Issues(null, null, null, null, null, null, null, issueState).TotalCount,
                        OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).TotalCount,

                        IsFork = x.IsFork,
                        IsInOrganization = x.IsInOrganization,
                        ViewerHasStarred = x.ViewerHasStarred,

                        UpdatedAt = x.UpdatedAt,
                        UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                        Owner = x.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
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
