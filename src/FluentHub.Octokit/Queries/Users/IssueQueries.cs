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

            var query = new Query()
                .User(login)
                .Issues(first: 30, orderBy: order)
                .Nodes
                .Select(x => new Issue
                {
                    Closed = x.Closed,
                    Number = x.Number,
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),

                    Comments = x.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
                    {
                        TotalCount = comments.TotalCount,
                    })
                    .SingleOrDefault(),

                    Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
                    {
                        Nodes = labels.Nodes.Select(y => new Label
                        {
                            Color = y.Color,
                            Description = y.Description,
                            Name = y.Name,
                        })
                        .ToList(),
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
