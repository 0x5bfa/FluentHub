namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueEventQueries
    {
        public async Task<List<object>> GetAllAsync(string owner, string name, int number)
        {
            #region queries
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
                    .Single(),

                    CreatedAt = y.CreatedAt,
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
                            Message = commit.Message,
                        })),

                        PullRequest = closer.Switch<PullRequest>(whenPr => whenPr
                        .PullRequest(pr => new PullRequest
                        {
                            Title = pr.Title,
                        })),
                    })
                    .SingleOrDefault(),

                    CreatedAt = y.CreatedAt,
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
                .ConvertedNoteToIssueEvent(y => new ConvertedNoteToIssueEvent
                {
                    Actor = y.Actor.Select(actor => new Actor
                    {
                        AvatarUrl = actor.AvatarUrl(100),
                        Login = actor.Login,
                    })
                    .Single(),

                    CreatedAt = y.CreatedAt,
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
                    .Single(),

                    MilestoneTitle = y.MilestoneTitle,

                    CreatedAt = y.CreatedAt,
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
                    .Single(),

                    AuthorAssociation = (CommentAuthorAssociation)y.AuthorAssociation,

                    Body = y.Body,

                    BodyHTML = y.BodyHTML,

                    CreatedAt = y.CreatedAt,

                    LastEditedAt = y.LastEditedAt,

                    MinimizedReason = y.MinimizedReason,

                    IsMinimized = y.IsMinimized,

                    Reactions = new()
                    {
                        Nodes = y.Reactions(6, null, null, null, null, null).Nodes.Select(reaction => new Reaction
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

                    LockReason = (LockReason)y.LockReason,
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
                        .Single(),

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
                    .Single(),

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
                    .Single(),

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
                    .Single(),

                    //StateReason = (IssueStateReason)y.StateReason,

                    CreatedAt = y.CreatedAt,
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

                    Id = y.Id,

                    CreatedAt = y.CreatedAt,
                })
                //.MentionedEvent(y => new MentionedEvent {})
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
