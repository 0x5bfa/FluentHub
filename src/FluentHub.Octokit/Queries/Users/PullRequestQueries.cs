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

            var query = new Query()
                .User(login)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new PullRequest
                {
                    BaseRefName = x.BaseRefName,
                    Closed = x.Closed,
                    HeadRefName = x.HeadRefName,
                    IsDraft = x.IsDraft,
                    Merged = x.Merged,
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

                    HeadRepository = x.HeadRepository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
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

                    Reviews = x.Reviews(null, null, 1, null, null, null).Select(reviews => new PullRequestReviewConnection
                    {
                        Nodes = reviews.Nodes.Select(y => new PullRequestReview
                        {
                            State = (PullRequestReviewState)y.State,
                        })
                        .ToList().DefaultIfEmpty().ToList(),
                    })
                    .SingleOrDefault(),

                    Commits = x.Commits(null, null, 1, null).Select(commits => new PullRequestCommitConnection
                    {
                        Nodes = commits.Nodes.Select(y => new PullRequestCommit
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
                        .ToList().DefaultIfEmpty().ToList(),
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
