namespace FluentHub.Octokit.Queries.Users
{
    public class IssueQueries
    {
        public async Task<List<Issue>> GetAllAsync(string login)
        {
            OctokitGraphQLModel.IssueOrder order = new()
            {
                Direction = OctokitGraphQLModel.OrderDirection.Desc,
                Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
            };

            #region query
            var query = new Query()
                .User(login)
                .Issues(first: 30, orderBy: order)
                .Nodes
                .Select(x => new Issue
                {
                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),

                    Comments = new()
                    {
                        TotalCount = x.Comments(null, null, null, null, null).TotalCount,
                    },

                    Labels = new()
                    {
                        Nodes = x.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                        {
                            Color = y.Color,
                            Description = y.Description,
                            Name = y.Name,
                        })
                        .ToList(),
                    },

                    Title = x.Title,
                    Closed = x.Closed,
                    Number = x.Number,
                    UpdatedAt = x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
