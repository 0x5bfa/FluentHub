namespace FluentHub.Octokit.Queries.Users
{
    public class PullRequestQueries
    {
        public async Task<List<PullRequest>> GetAllAsync(string login)
        {
            OctokitGraphQLModel.IssueOrder order = new()
            {
                Direction = OctokitGraphQLModel.OrderDirection.Desc,
                Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
            };

            #region queries
            var query = new Query()
                .User(login)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new PullRequest
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

                    HeadRepository = x.HeadRepository.Select(repo => new Repository
                    {
                        Name = repo.Name,
                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
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

                    Reviews = new()
                    {
                        Nodes = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => new PullRequestReview
                        {
                            State = (PullRequestReviewState)y.State,
                        })
                        .ToList()
                    },

                    Commits = new()
                    {
                        Nodes = x.Commits(null, null, 1, null).Nodes.Select(y => new PullRequestCommit
                        {
                            Commit = y.Commit.Select(commit => new Commit
                            {
                                StatusCheckRollup = commit.StatusCheckRollup.Select(rollup => new StatusCheckRollup
                                {
                                    State = (StatusState)rollup.State,
                                })
                                .SingleOrDefault(),
                            })
                            .SingleOrDefault(),
                        })
                        .ToList()
                    },

                    Title = x.Title,
                    BaseRefName = x.BaseRefName,
                    HeadRefName = x.HeadRefName,
                    Closed = x.Closed,
                    Merged = x.Merged,
                    IsDraft = x.IsDraft,
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
