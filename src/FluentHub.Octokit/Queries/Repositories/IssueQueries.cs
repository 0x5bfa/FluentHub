namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueQueries
    {
        public async Task<List<Issue>> GetAllAsync(string name, string owner)
        {
            OctokitGraphQLModel.IssueOrder order = new()
            {
                Direction = OctokitGraphQLModel.OrderDirection.Desc,
                Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
            };

            #region queries
            var query = new Query()
                .Repository(name, owner)
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

        public async Task<Issue> GetAsync(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
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

                    Title = x.Title,
                    Closed = x.Closed,
                    Number = x.Number,

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

                    UpdatedAt = x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response;
        }

        public async Task<Issue> GetDetailsAsync(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    Assignees = x.Assignees(6, null, null, null).Nodes.Select(assignees => new {
                        assignees.Login,
                        AvatarUrl = assignees.AvatarUrl(100),
                    }),

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(labels => new
                    {
                        labels.Name,
                        labels.Description,
                        labels.Color,
                    }),

                    Projects = x.ProjectCards(6, null, null, null, null).Nodes.Select(projects => new
                    {
                        projects.Note,
                    }),

                    x.Milestone,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return null;
        }

        public async Task<IssueComment> GetBodyAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
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

        public async Task<List<Issue>> GetPinnedAllAsync(string owner, string name)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .PinnedIssues(3, null, null, null)
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

                    Title = x.Issue.Title,
                    Closed = x.Issue.Closed,
                    Number = x.Issue.Number,

                    Comments = new()
                    {
                        TotalCount = x.Issue.Comments(null, null, null, null, null).TotalCount,
                    },

                    Labels = new()
                    {
                        Nodes = x.Issue.Labels(10, null, null, null, null).Nodes.Select(y => new Label
                        {
                            Color = y.Color,
                            Description = y.Description,
                            Name = y.Name,
                        })
                        .ToList(),
                    },

                    UpdatedAt = x.Issue.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
