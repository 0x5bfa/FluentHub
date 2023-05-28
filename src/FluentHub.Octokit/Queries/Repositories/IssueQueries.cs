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

            var query = new Query()
                .Repository(name, owner)
                .Issues(first: 30, orderBy: order)
                .Nodes
                .Select(x => new Issue
                {
                    Closed = x.Closed,
                    Number = x.Number,
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(500),
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

        public async Task<Issue> GetAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new Issue
                {
                    Closed = x.Closed,
                    Number = x.Number,
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,

                    Assignees = x.Assignees(6, null, null, null).Select(assignees => new UserConnection
                    {
                        Nodes = assignees.Nodes.Select(y => new User
                        {
                            AvatarUrl = y.AvatarUrl(500),
                            Login = y.Login,
                        })
                        .ToList(),
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

                    Milestone = x.Milestone.Select(y => new Milestone
                    {
                        Title = y.Title,
                        ProgressPercentage = y.ProgressPercentage,
                    })
                    .SingleOrDefault(),

                    Participants = x.Participants(6, null, null, null).Select(participants => new UserConnection
                    {
                        Nodes = participants.Nodes.Select(y => new User
                        {
                            AvatarUrl = y.AvatarUrl(500),
                            Login = y.Login,
                        })
                        .ToList(),
                    })
                    .SingleOrDefault(),

                    ProjectCards = x.ProjectCards(6, null, null, null, null).Select(projects => new ProjectCardConnection
                    {
                        Nodes = projects.Nodes.Select(y => new ProjectCard
                        {
                            Note = y.Note,
                        })
                        .ToList(),
                    })
                    .SingleOrDefault(),

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(500),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),
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
                .Issue(number)
                .Select(x => new IssueComment
                {
                    AuthorAssociation = (CommentAuthorAssociation)x.AuthorAssociation,
                    Body = x.Body,
                    BodyHTML = x.BodyHTML,
                    CreatedAt = x.CreatedAt,
                    CreatedAtHumanized = x.CreatedAt.Humanize(null, null),
                    Id = x.Id,
                    LastEditedAt = x.LastEditedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
                    Url = x.Url,
                    ViewerCanReact = x.ViewerCanReact,
                    ViewerCanUpdate = x.ViewerCanUpdate,
                    ViewerDidAuthor = x.ViewerDidAuthor,

                    Author = x.Author.Select(author => new Actor
                    {
                        Login = author.Login,
                        AvatarUrl = author.AvatarUrl(500),
                    })
                    .SingleOrDefault(),

                    Reactions = x.Reactions(100, null, null, null, null, null).Select(reactions => new ReactionConnection
                    {
                        Nodes = reactions.Nodes.Select(reaction => new Reaction
                        {
                            Content = (ReactionContent)reaction.Content,

                            User = reaction.User.Select(user => new User
                            {
                                Login = user.Login,
                            })
                            .SingleOrDefault(),
                        })
                        .ToList(),
                    })
                    .SingleOrDefault(),
                })
                .Compile();

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
                    Closed = x.Issue.Closed,
                    Number = x.Issue.Number,
                    Title = x.Issue.Title,
                    UpdatedAt = x.Issue.UpdatedAt,

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(500),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),

                    Comments = x.Issue.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
                    {
                        TotalCount = comments.TotalCount,
                    })
                    .SingleOrDefault(),

                    Labels = x.Issue.Labels(10, null, null, null, null).Select(labels => new LabelConnection
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
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
