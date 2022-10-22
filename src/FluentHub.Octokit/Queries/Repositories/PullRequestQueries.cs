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
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

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
                    Additions = x.Additions,
                    BaseRefName = x.BaseRefName,
                    ChangedFiles = x.ChangedFiles,
                    Closed = x.Closed,
                    Deletions = x.Deletions,
                    HeadRefName = x.HeadRefName,
                    IsDraft = x.IsDraft,
                    Merged = x.Merged,
                    Number = x.Number,
                    Title = x.Title,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                    Assignees = x.Assignees(6, null, null, null).Select(assignees => new UserConnection
                    {
                        Nodes = assignees.Nodes.Select(y => new User
                        {
                            AvatarUrl = y.AvatarUrl(100),
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

                    Commits = x.Commits(null, null, 1, null).Select(commits => new PullRequestCommitConnection
                    {
                        TotalCount = commits.TotalCount,

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

                    LatestReviews = x.LatestReviews(15, null, null, null).Select(latestReviews => new PullRequestReviewConnection
                    {
                        Nodes = latestReviews.Nodes.Select(latestReview => new PullRequestReview
                        {
                            Author = latestReview.Author.Select(author => new Actor
                            {
                                AvatarUrl = author.AvatarUrl(100),
                                Login = author.Login,
                            })
                            .SingleOrDefault(),
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
                            AvatarUrl = y.AvatarUrl(100),
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
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),

                    ReviewRequests = x.ReviewRequests(15, null, null, null).Select(reviewRequests => new ReviewRequestConnection
                    {
                        Nodes = reviewRequests.Nodes.Select(reviewRequest => new ReviewRequest
                        {
                            RequestedReviewer = reviewRequest.RequestedReviewer.Select(requestedReviewer => new RequestedReviewer
                            {
                                User = requestedReviewer.Switch<User>(whenUser => whenUser
                                .User(user => new User
                                {
                                    AvatarUrl = user.AvatarUrl(100),
                                    Login = user.Login,
                                })),
                            })
                            .SingleOrDefault(),
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
                        AvatarUrl = author.AvatarUrl(100),
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
    }
}
