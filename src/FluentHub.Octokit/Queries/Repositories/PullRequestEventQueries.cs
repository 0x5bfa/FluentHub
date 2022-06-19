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
                    .Single(),

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
                    .Single(),

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
                .AutoMergeDisabledEvent(y => new AutoMergeDisabledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

                    Reason = y.Reason,

                    ReasonCode = y.ReasonCode,
                })
                .AutoMergeEnabledEvent(y => new AutoMergeEnabledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .AutoRebaseEnabledEvent(y => new AutoRebaseEnabledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .AutoSquashEnabledEvent(y => new AutoSquashEnabledEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .AutomaticBaseChangeFailedEvent(y => new AutomaticBaseChangeFailedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

                    NewBase = y.NewBase,

                    OldBase = y.OldBase,
                })
                .AutomaticBaseChangeSucceededEvent(y => new AutomaticBaseChangeSucceededEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

                    NewBase = y.NewBase,

                    OldBase = y.OldBase,
                })
                .BaseRefChangedEvent(y => new BaseRefChangedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    CurrentRefName = y.CurrentRefName,

                    Id = y.Id.ToString(),

                    PreviousRefName = y.PreviousRefName,
                })
                .BaseRefDeletedEvent(y => new BaseRefDeletedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    BaseRefName = y.BaseRefName,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),
                })
                .BaseRefForcePushedEvent(y => new BaseRefForcePushedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    AfterCommit = y.AfterCommit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .SingleOrDefault(),

                    BeforeCommit = y.BeforeCommit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),
                })
                .ClosedEvent(y => new ClosedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    Closer = y.Closer.Select(closer => new Closer
                    {
                        Commit = closer.Switch<Commit>(whenCommit => whenCommit
                        .Commit(commit => new Commit
                        {
                            CommitMessage = commit.Message,
                        })),

                        PullRequest = closer.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

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
                    .Single(),

                    DeletedCommentAuthor = y.DeletedCommentAuthor.Select(deletedAuthor => new Actor
                    {
                        AvatarUrl = deletedAuthor.AvatarUrl(100),
                        Login = deletedAuthor.Login,
                    })
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

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
                .ConvertedNoteToIssueEvent(y => new ConvertedNoteToIssueEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

                    IsCrossRepository = y.IsCrossRepository,

                    ReferencedAt = y.ReferencedAt,
                    ReferencedAtHumanized = y.ReferencedAt.Humanize(null, null),

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
                    .Single(),

                    MilestoneTitle = y.MilestoneTitle,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .DeployedEvent(y => new DeployedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Deployment = y.Deployment.Select(deployment => new Deployment
                    {
                        Description = deployment.Description,
                    })
                    .Single(),

                    Id = y.Id.ToString(),

                    Ref = y.Ref.Select(ref1 => new Ref
                    {
                        Name = ref1.Name,
                    })
                    .Single(),
                })
                .DeploymentEnvironmentChangedEvent(y => new DeploymentEnvironmentChangedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    DeploymentStatus = y.DeploymentStatus.Select(depStatus => new DeploymentStatus
                    {
                        Description = depStatus.Description,
                    })
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    HeadRef = y.HeadRef.Select(headRef => new Ref
                    {
                        Name = headRef.Name,
                    })
                    .Single(),

                    HeadRefName = y.HeadRefName,
                })
                .HeadRefForcePushedEvent(y => new HeadRefForcePushedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    AfterCommit = y.AfterCommit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .SingleOrDefault(),

                    BeforeCommit = y.BeforeCommit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),
                })
                .HeadRefRestoredEvent(y => new HeadRefRestoredEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .IssueComment(y => new IssueComment
                {
                    Author = y.Author.Select(author => new Actor
                    {
                        AvatarUrl = author.AvatarUrl(100),
                        Login = author.Login,
                    })
                    .Single(),

                    AuthorAssociation = (CommentAuthorAssociation)y.AuthorAssociation,

                    Body = y.Body,

                    BodyHTML = y.BodyHTML,

                    CreatedAt = y.CreatedAt,

                    LastEditedAt = y.LastEditedAt,

                    MinimizedReason = y.MinimizedReason,

                    IsMinimized = y.IsMinimized,

                    Reactions = y.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new Reaction
                    {
                        Content = reaction.Content,
                        ReactorLogin = reaction.User.Login,
                    })
                    .ToList(),

                    UpdatedAt = y.UpdatedAt,

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
                    .Single(),

                    Label = y.Label.Select(label => new Label
                    {
                        Color = label.Color,
                        Description = label.Description,
                        Name = label.Name,
                    })
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    LockReason = y.LockReason,
                })
                .MarkedAsDuplicateEvent(y => new MarkedAsDuplicateEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                .MergedEvent(y => new MergedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    MergeRef = y.MergeRef.Select(mergeRef => new Ref
                    {
                        Name = mergeRef.Name,
                    })
                    .Single(),

                    MergeRefName = y.MergeRefName,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .MilestonedEvent(y => new MilestonedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .PullRequestCommit(y => new PullRequestCommit
                {
                    Actor = y.Commit.Author.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Name,
                    })
                    .Single(),

                    Commit = y.Commit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .Single(),
                })
                .PullRequestReview(y => new PullRequestReview
                {
                    Actor = y.Commit.Author.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Name,
                    })
                    .Single(),

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
                .ReadyForReviewEvent(y => new ReadyForReviewEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    .Single(),

                    Commit = y.Commit.Select(commit => new Commit
                    {
                        CommitMessage = commit.Message,
                    })
                    .SingleOrDefault(),

                    CommitRepository = y.CommitRepository.Select(from => new Repository
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
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    Id = y.Id.ToString(),

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
                    .Single(),

                    Id = y.Id.ToString(),

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
                    .Single(),

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
                    .Single(),

                    //StateReason = (IssueStateReason)y.StateReason,

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ReviewDismissedEvent(y => new ReviewDismissedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),

                    DismissalMessage = y.DismissalMessage,
                })
                .ReviewRequestRemovedEvent(y => new ReviewRequestRemovedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .ReviewRequestedEvent(y => new ReviewRequestedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .TransferredEvent(y => new TransferredEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

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
                    .Single(),

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
                    .Single(),

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
                    .Single(),

                    Label = y.Label.Select(label => new Label
                    {
                        Color = label.Color,
                        Description = label.Description,
                        Name = label.Name,
                    })
                    .Single(),

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
                    .Single(),

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
                    .Single(),

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
                    .Single(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                .UserBlockedEvent(y => new UserBlockedEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    BlockDuration = (UserBlockDuration)y.BlockDuration,

                    Id = y.Id.ToString(),

                    CreatedAt = y.CreatedAt,
                    CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
                })
                //.MentionedEvent(y => new MentionedEvent {})
                //.PullRequestCommitCommentThread(y => new PullRequestCommitCommentThread {})
                //.PullRequestReviewThread(y => new PullRequestReviewThread {})
                //.PullRequestRevisionMarker(y => new PullRequestRevisionMarker {})
                //.SubscribedEvent(y => new SubscribedEvent {})
                //.UnsubscribedEvent(y => new UnsubscribedEvent {})
                ))
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
