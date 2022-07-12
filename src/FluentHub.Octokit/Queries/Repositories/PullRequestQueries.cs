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

            #region queries
            var query = new Query()
                .Repository(name, owner)
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
                        .ToList(),
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

            var res = await App.Connection.Run(query);

            return res.ToList();
        }

        public async Task<PullRequest> GetAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
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

            var res = await App.Connection.Run(query);

            return res;
        }

        public async Task<IssueComment> GetBodyAsync(string owner, string name, int number)
        {
            #region queries
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
            #endregion

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
