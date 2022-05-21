namespace FluentHub.Octokit.Queries.Users
{
    public class PullRequestQueries
    {
        public PullRequestQueries() => new App();

        public async Task<List<PullRequest>> GetAllAsync(string login)
        {
            OctokitGraphQLModel.IssueOrder order = new() { Direction = OctokitGraphQLModel.OrderDirection.Desc, Field = OctokitGraphQLModel.IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .User(login)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new PullRequest
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    Name = x.Repository.Name,
                    Title = x.Title,

                    BaseRefName = x.BaseRefName,
                    HeadRefName = x.HeadRefName,
                    HeadRefOwnerLogin = x.HeadRepositoryOwner.Login,

                    Closed = x.Closed,
                    Merged = x.Merged,
                    IsDraft = x.IsDraft,

                    Number = x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                    {
                        Color = y.Color,
                        Description = y.Description,
                        Name = y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => y.State)
                    .ToList().FirstOrDefault(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => y.Commit.StatusCheckRollup.Select(z => new StatusCheckRollup
                    {
                        Status = z.State,
                    })
                    .SingleOrDefault())
                    .ToList().FirstOrDefault(),

                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
