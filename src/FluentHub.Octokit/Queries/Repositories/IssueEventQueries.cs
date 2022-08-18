namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueEventQueries
    {
        public async Task<List<object>> GetAllAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .TimelineItems(40, null, null, null, null, null, null)
                .Nodes
                .Select(node => node.Switch<object>(when => when
                .AddedToProjectEvent(y => new AddedToProjectEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .AssignedEvent(y => new AssignedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Assignee = y.Assignee.Select(assignee => new Assignee
                    {
                        User = assignee.Switch<User>(whenUser => whenUser
                        .User(user => new User
                        {
                            AvatarUrl = user.AvatarUrl(100),
                            Login = user.Login,
                        })),
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ClosedEvent(y => new ClosedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Closer = y.Closer.Select(closer => new Closer
                    {
                        Commit = closer.Switch<Commit>(whenCommit => whenCommit
                        .Commit(commit => new Commit
                        {
                            Message = commit.Message,
                        })),

                        PullRequest = closer.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    StateReason = (IssueStateReason)y.StateReason,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .CommentDeletedEvent(y => new CommentDeletedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    DeletedCommentAuthor = y.DeletedCommentAuthor.Select(deletedAuthor => new Actor
                    {
                        AvatarUrl = deletedAuthor.AvatarUrl(100),
                        Login = deletedAuthor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ConnectedEvent(y => new ConnectedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,

                    IsCrossRepository = y.IsCrossRepository,

                    Source = y.Source.Select(source => new ReferencedSubject
                    {
                        Issue = source.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = source.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    Subject = y.Subject.Select(subject => new ReferencedSubject
                    {
                        Issue = subject.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = subject.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),
                })
                .ConvertedToDiscussionEvent(y => new ConvertedToDiscussionEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,
                })
                .ConvertedNoteToIssueEvent(y => new ConvertedNoteToIssueEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .CrossReferencedEvent(y => new CrossReferencedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,

                    IsCrossRepository = y.IsCrossRepository,

                    ReferencedAt = y.ReferencedAt,

                    Source = y.Source.Select(source => new ReferencedSubject
                    {
                        Issue = source.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = source.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    Target = y.Target.Select(target => new ReferencedSubject
                    {
                        Issue = target.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = target.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    Url = y.Url,

                    WillCloseTarget = y.WillCloseTarget,
                })
                .DemilestonedEvent(y => new DemilestonedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    MilestoneTitle = y.MilestoneTitle,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .DisconnectedEvent(y => new DisconnectedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,

                    IsCrossRepository = y.IsCrossRepository,

                    Source = y.Source.Select(source => new ReferencedSubject
                    {
                        Issue = source.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = source.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    Subject = y.Subject.Select(subject => new ReferencedSubject
                    {
                        Issue = subject.Switch<Issue>(whenIssue => whenIssue
                        .Issue(issue => new Issue
                        {
                            Title = issue.Title,
                        })),

                        PullRequest = subject.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),
                })
                .IssueComment(y => new IssueComment
                {
                    Author = y.Author.Select(author => new Actor
                    {
                        AvatarUrl = author.AvatarUrl(100),
                        Login = author.Login,
                    })
                    .SingleOrDefault(),

                    AuthorAssociation = (CommentAuthorAssociation)y.AuthorAssociation,

                    Body = y.Body,

                    BodyHTML = y.BodyHTML,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    LastEditedAt = y.LastEditedAt,

                    MinimizedReason = y.MinimizedReason,

                    IsMinimized = y.IsMinimized,

                    Id = y.Id,

                    Reactions = new()
                    {
                        Nodes = y.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new Reaction
                        {
                            Content = (ReactionContent)reaction.Content,
                            User = reaction.User.Select(user => new User
                            {
                                Login = user.Login,
                            })
                            .SingleOrDefault(),
                        })
                        .ToList(),
                    },

                    UpdatedAt = y.UpdatedAt,
                    UpdatedAtHumanized = y.UpdatedAt.Humanize(null, null),

                    Url = y.Url,

                    ViewerCanDelete = y.ViewerCanDelete,

                    ViewerCanMinimize = y.ViewerCanMinimize,

                    ViewerCanReact = y.ViewerCanReact,

                    ViewerCanUpdate = y.ViewerCanUpdate,

                    ViewerDidAuthor = y.ViewerDidAuthor,
                })
                .LabeledEvent(y => new LabeledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Label = y.Label.Select(label => new Label
                    {
                        Color = label.Color,
                        Description = label.Description,
                        Name = label.Name,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .LockedEvent(y => new LockedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    LockReason = (LockReason)y.LockReason,
                })
                .MarkedAsDuplicateEvent(y => new MarkedAsDuplicateEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Duplicate = y.Duplicate.Select(a => new IssueOrPullRequest
                    {
                        Issue = a.Switch<Issue>(i => i
                        .Issue(j => new Issue
                        {
                            Title = j.Title,
                        })),

                        PullRequest = a.Switch<PullRequest>(b => b
                        .PullRequest(p => new PullRequest
                        {
                            Title = p.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .MentionedEvent(y => new  MentionedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,
                })
                .MilestonedEvent(y => new MilestonedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    MilestoneTitle = y.MilestoneTitle,
                })
                .MovedColumnsInProjectEvent(y => new MovedColumnsInProjectEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .PinnedEvent(y => new PinnedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ReferencedEvent(y => new ReferencedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Commit = y.Commit.Select(commit => new Commit
                    {
                        Message = commit.Message,
                    })
                    .SingleOrDefault(),

                    CommitRepository = y.CommitRepository.Select(from => new Repository
                    {
                        Owner = from.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),

                        Name = from.Name,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,

                    IsCrossRepository = y.IsCrossRepository,

                    IsDirectReference = y.IsDirectReference,
                })
                .RemovedFromProjectEvent(y => new RemovedFromProjectEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Id = y.Id,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .RenamedTitleEvent(y => new RenamedTitleEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CurrentTitle = y.CurrentTitle,

                    PreviousTitle = y.PreviousTitle,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ReopenedEvent(y => new ReopenedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    StateReason = (IssueStateReason)y.StateReason,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .SubscribedEvent(y => new SubscribedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,
                })
                .TransferredEvent(y => new TransferredEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    FromRepository = y.FromRepository.Select(from => new Repository
                    {
                        Owner = from.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),

                        Name = from.Name,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnassignedEvent(y => new UnassignedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Assignee = y.Assignee.Select(assignee => new Assignee
                    {
                        User = assignee.Switch<User>(whenUser => whenUser
                        .User(user => new User
                        {
                            AvatarUrl = user.AvatarUrl(100),
                            Login = user.Login,
                        }))
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnlabeledEvent(y => new UnlabeledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Label = y.Label.Select(label => new Label
                    {
                        Color = label.Color,
                        Description = label.Description,
                        Name = label.Name,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnlockedEvent(y => new UnlockedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnmarkedAsDuplicateEvent(y => new UnmarkedAsDuplicateEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    Duplicate = y.Duplicate.Select(a => new IssueOrPullRequest
                    {
                        Issue = a.Switch<Issue>(i => i
                        .Issue(j => new Issue
                        {
                            Title = j.Title,
                        })),

                        PullRequest = a.Switch<PullRequest>(b => b
                        .PullRequest(p => new PullRequest
                        {
                            Title = p.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnpinnedEvent(y => new UnpinnedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UnsubscribedEvent(y => new UnsubscribedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id,
                })
                .UserBlockedEvent(y => new UserBlockedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    BlockDuration = (UserBlockDuration)y.BlockDuration,

                    Id = y.Id,

                    Subject = y.Subject.Select(user => new User
                    {
                        Login = user.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                ))
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
