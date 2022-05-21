namespace FluentHub.Octokit.Queries.Users
{
    public class PinnedItemQueries
    {
        public PinnedItemQueries() => new App();

        public async Task<List<Repository>> GetAllAsync(string login)
        {
            OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState = new(new OctokitGraphQLModel.IssueState[] { OctokitGraphQLModel.IssueState.Open });
            OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState = new(new OctokitGraphQLModel.PullRequestState[] { OctokitGraphQLModel.PullRequestState.Open });

            #region query
            var query = new Query()
                    .User(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .OfType<OctokitGraphQLModel.Repository>()
                    .Select(x => new Repository
                    {
                        Name = x.Name,
                        Description = x.Description,

                        StargazerCount = x.StargazerCount,
                        ViewerHasStarred = x.ViewerHasStarred,

                        PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
                        {
                            Name = y.Name,
                            Color = y.Color,
                        })
                        .SingleOrDefault(),

                        Owner = x.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
