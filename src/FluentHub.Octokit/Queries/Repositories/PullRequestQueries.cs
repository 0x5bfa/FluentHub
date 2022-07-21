namespace FluentHub.Octokit.Queries.Repositories
{
    public class PullRequestQueries
    {
        public async Task<List<PullRequest>> GetAllAsync(string name, string owner)
        {
            OctokitGraphQLModel.IssueOrder order = new()
            {
                Direction = OctokitGraphQLModel.OrderDirection.Desc,
                Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
            };

            var query = new Query()
                .Repository(name, owner)
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

        public async Task<PullRequest> GetAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
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

            return response;
        }

        public async Task<IssueComment> GetBodyAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Select(x => new IssueComment
                {
                    Author = x.Author.Select(author => new Actor
                    {
                        Login = author.Login,
                        AvatarUrl = author.AvatarUrl(100),
                    })
                    .Single(),

                    Reactions = new()
                    {
                        Nodes = x.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new Reaction
                        {
                            Content = (ReactionContent)reaction.Content,
                            User = reaction.User.Select(user => new User
                            {
                                Login = user.Login,
                            })
                            .Single(),
                        })
                        .ToList(),
                    },

                    AuthorAssociation = (CommentAuthorAssociation)x.AuthorAssociation,
                    Body = x.Body,
                    BodyHTML = x.BodyHTML,
                    LastEditedAt = x.LastEditedAt,
                    UpdatedAt = x.UpdatedAt,
                    ViewerCanReact = x.ViewerCanReact,
                    ViewerCanUpdate = x.ViewerCanUpdate,
                    ViewerDidAuthor = x.ViewerDidAuthor,
                    Url = x.Url,
                    CreatedAt = x.CreatedAt,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
