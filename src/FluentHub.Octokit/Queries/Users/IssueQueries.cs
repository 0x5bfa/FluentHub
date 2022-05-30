namespace FluentHub.Octokit.Queries.Users
{
    public class IssueQueries
    {
        public IssueQueries() => new App();

        public async Task<List<Issue>> GetAllAsync(string login)
        {
            OctokitGraphQLModel.IssueOrder order = new() { Direction = OctokitGraphQLModel.OrderDirection.Desc, Field = OctokitGraphQLModel.IssueOrderField.CreatedAt };

            #region query
            var query = new Query()
                .User(login)
                .Issues(first: 30, orderBy: order)
                .Nodes
                .Select(x => new Issue
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    Name = x.Repository.Name,
                    Title = x.Title,

                    Closed = x.Closed,

                    Number = x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                    {
                        Color = y.Color,
                        Description = y.Description,
                        Name = y.Name,
                    })
                    .ToList(),

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
