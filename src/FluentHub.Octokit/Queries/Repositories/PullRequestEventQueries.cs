namespace FluentHub.Octokit.Queries.Repositories
{
    public class PullRequestEventQueries
    {
        public async Task<List<object>> GetAllAsync(string owner, string name, int number)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
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
                        }))
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                })
                .AutoMergeDisabledEvent(y => new AutoMergeDisabledEvent
                {
                    CreatedAt = y.CreatedAt,
                    Id = y.Id,
                    Reason = y.Reason,
                    ReasonCode = y.ReasonCode,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .AutoMergeEnabledEvent(y => new AutoMergeEnabledEvent
                {
                    CreatedAt = y.CreatedAt,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .AutoRebaseEnabledEvent(y => new AutoRebaseEnabledEvent
                {
                    CreatedAt = y.CreatedAt,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .AutoSquashEnabledEvent(y => new AutoSquashEnabledEvent
                {
                    CreatedAt = y.CreatedAt,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .AutomaticBaseChangeFailedEvent(y => new AutomaticBaseChangeFailedEvent
                {
                    CreatedAt = y.CreatedAt,
                    Id = y.Id,
                    NewBase = y.NewBase,
                    OldBase = y.OldBase,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .AutomaticBaseChangeSucceededEvent(y => new AutomaticBaseChangeSucceededEvent
                {
                    CreatedAt = y.CreatedAt,
                    Id = y.Id,
                    NewBase = y.NewBase,
                    OldBase = y.OldBase,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .BaseRefChangedEvent(y => new BaseRefChangedEvent
                {
                    CreatedAt = y.CreatedAt,
                    CurrentRefName = y.CurrentRefName,
                    Id = y.Id,
                    PreviousRefName = y.PreviousRefName,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .BaseRefDeletedEvent(y => new BaseRefDeletedEvent
                {
                    BaseRefName = y.BaseRefName,
                    CreatedAt = y.CreatedAt,
                    Id = y.Id,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),
                })
                .BaseRefForcePushedEvent(y => new BaseRefForcePushedEvent
                {
                    CreatedAt = y.CreatedAt,
                    Id = y.Id,

                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    AfterCommit = y.AfterCommit.Select(commit => new Commit
                    {
                        Message = commit.Message,
                    })
                    .SingleOrDefault(),

                    BeforeCommit = y.BeforeCommit.Select(commit => new Commit
                    {
                        Message = commit.Message,
                    })
                    .SingleOrDefault(),
                })
                .ClosedEvent(y => new ClosedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

                    Id = y.Id,
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
                .ConvertToDraftEvent(y => new ConvertToDraftEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

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
                })
                .ConvertedToDiscussionEvent(y => new ConvertedToDiscussionEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

                    Id = y.Id,
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
                })
                .DeployedEvent(y => new DeployedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,

                    Deployment = y.Deployment.Select(deployment => new Deployment
                    {
                        Description = deployment.Description,
                    })
                    .SingleOrDefault(),

                    Id = y.Id,

                    Ref = y.Ref.Select(ref1 => new Ref
                    {
                        Name = ref1.Name,
                    })
                    .SingleOrDefault(),
                })
                .DeploymentEnvironmentChangedEvent(y => new DeploymentEnvironmentChangedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    DeploymentStatus = y.DeploymentStatus.Select(depStatus => new DeploymentStatus
                    {
                        Description = depStatus.Description,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
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
                .HeadRefDeletedEvent(y => new HeadRefDeletedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,

                    HeadRef = y.HeadRef.Select(headRef => new Ref
                    {
                        Name = headRef.Name,
                    })
                    .SingleOrDefault(),

                    HeadRefName = y.HeadRefName,
                })
                .HeadRefForcePushedEvent(y => new HeadRefForcePushedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    AfterCommit = y.AfterCommit.Select(commit => new Commit
                    {
                        Message = commit.Message,
                    })
                    .SingleOrDefault(),

                    BeforeCommit = y.BeforeCommit.Select(commit => new Commit
                    {
                        Message = commit.Message,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,

                    Id = y.Id,
                })
                .HeadRefRestoredEvent(y => new HeadRefRestoredEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                })
                .IssueComment(y => new IssueComment
                {
                    AuthorAssociation = (CommentAuthorAssociation)y.AuthorAssociation,
                    Body = y.Body,
                    BodyHTML = y.BodyHTML,
                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                    Id = y.Id,
                    IsMinimized = y.IsMinimized,
                    LastEditedAt = y.LastEditedAt,
                    MinimizedReason = y.MinimizedReason,
                    UpdatedAt = y.UpdatedAt,
                    UpdatedAtHumanized = y.UpdatedAt.Humanize(null, null),
                    Url = y.Url,
                    ViewerCanDelete = y.ViewerCanDelete,
                    ViewerCanMinimize = y.ViewerCanMinimize,
                    ViewerCanReact = y.ViewerCanReact,
                    ViewerCanUpdate = y.ViewerCanUpdate,
                    ViewerDidAuthor = y.ViewerDidAuthor,

                    Author = y.Author.Select(author => new Actor
                    {
                        AvatarUrl = author.AvatarUrl(100),
                        Login = author.Login,
                    })
                    .SingleOrDefault(),

                    Reactions = y.Reactions(100, null, null, null, null, null).Select(reactions => new ReactionConnection
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
                })
                .MentionedEvent(y => new MentionedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

                    Id = y.Id,
                })
                .MergedEvent(y => new MergedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    MergeRef = y.MergeRef.Select(mergeRef => new Ref
                    {
                        Name = mergeRef.Name,
                    })
                    .SingleOrDefault(),

                    MergeRefName = y.MergeRefName,

                    CreatedAt = y.CreatedAt,
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
                })
                .PullRequestCommit(y => new PullRequestCommit
                {
                    Commit = new()
                    {
                        Author = y.Commit.Author.Select(actor => new GitActor
                        {
                            AvatarUrl = actor.AvatarUrl(100),
                            User = actor.User.Select(user => new User
                            {
                                Login = user.Login,
                            })
                            .SingleOrDefault(),
                        })
                        .SingleOrDefault(),

                        Message = y.Commit.Message,
                    },
                })
                .PullRequestCommitCommentThread(y => new PullRequestCommitCommentThread
                {
                    Id = y.Id,
                })
                .PullRequestReview(y => new PullRequestReview
                {
                    Commit = new()
                    {
                        Author = y.Commit.Author.Select(actor => new GitActor
                        {
                            AvatarUrl = actor.AvatarUrl(100),
                            User = actor.User.Select(user => new User
                            {
                                Login = user.Login,
                            })
                            .Single(),
                        })
                        .Single(),
                    },

                    AuthorAssociation = (CommentAuthorAssociation)y.AuthorAssociation,
                    Body = y.Body,
                    BodyHTML = y.BodyHTML,
                    CreatedAt = y.CreatedAt,
                    LastEditedAt = y.LastEditedAt,
                    UpdatedAt = y.UpdatedAt,
                    Url = y.Url,
                    ViewerCanDelete = y.ViewerCanDelete,
                    ViewerCanReact = y.ViewerCanReact,
                    ViewerCanUpdate = y.ViewerCanUpdate,
                    ViewerDidAuthor = y.ViewerDidAuthor,
                })
                .PullRequestReviewThread(y => new PullRequestReviewThread
                {
                    Id = y.Id,
                })
                .PullRequestRevisionMarker(y => new PullRequestRevisionMarker
                {
                    CreatedAt = y.CreatedAt,
                })
                .ReadyForReviewEvent(y => new ReadyForReviewEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
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
                })
                .ReviewDismissedEvent(y => new ReviewDismissedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,

                    DismissalMessage = y.DismissalMessage,
                })
                .ReviewRequestRemovedEvent(y => new ReviewRequestRemovedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    RequestedReviewer = y.RequestedReviewer.Select(reviewer => new RequestedReviewer
                    {
                        User = reviewer.Switch<User>(whenUser => whenUser
                        .User(user => new User
                        {
                            AvatarUrl = user.AvatarUrl(100),
                            Login = user.Login,
                        }))
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                })
                .ReviewRequestedEvent(y => new ReviewRequestedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .SingleOrDefault(),

                    RequestedReviewer = y.RequestedReviewer.Select(reviewer => new RequestedReviewer
                    {
                        User = reviewer.Switch<User>(whenUser => whenUser
                        .User(user => new User
                        {
                            AvatarUrl = user.AvatarUrl(100),
                            Login = user.Login,
                        }))
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                })
                .SubscribedEvent(y => new SubscribedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

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
                        .Single(),

                        Name = from.Name,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
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
                })
                .UnsubscribedEvent(y => new UnsubscribedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,

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

                    CreatedAt = y.CreatedAt,
                })
                ))
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
