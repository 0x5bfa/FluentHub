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
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.AssignedEvent(y => new AssignedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Assignee = y.Assignee.Select(assignee => new Assignee
					{
						Bot = assignee.Switch<Bot>(whenBot => whenBot
						.Bot(bot => new Bot
						{
							Login = bot.Login,
						})),
						Mannequin = assignee.Switch<Mannequin>(whenMannequin => whenMannequin
						.Mannequin(mannequin => new Mannequin
						{
							Login = mannequin.Login,
						})),
						Organization = assignee.Switch<Organization>(whenOrganization => whenOrganization
						.Organization(organization => new Organization
						{
							Login = organization.Login,
						})),
						User = assignee.Switch<User>(whenUser => whenUser
						.User(user => new User
						{
							Login = user.Login,
						})),
					})
					.SingleOrDefault(),

					//[Obsolete]
					//User = y.User.Select(user => new User
					//{
					//	Login = user.Login,
					//})
					//.SingleOrDefault(),
				})
				.ClosedEvent(y => new ClosedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					StateReason = (IssueStateReason)y.StateReason,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
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
				})
				.CommentDeletedEvent(y => new CommentDeletedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					DeletedCommentAuthor = y.DeletedCommentAuthor.Select(deletedCommentAuthor => new Actor
					{
						AvatarUrl = deletedCommentAuthor.AvatarUrl(500),
						Login = deletedCommentAuthor.Login,
					})
					.SingleOrDefault(),
				})
				.ConnectedEvent(y => new ConnectedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					IsCrossRepository = y.IsCrossRepository,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Source = y.Source.Select(source => new ReferencedSubject
					{
						Issue = source.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = source.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),

					Subject = y.Subject.Select(subject => new ReferencedSubject
					{
						Issue = subject.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = subject.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),
				})
				.ConvertedNoteToIssueEvent(y => new ConvertedNoteToIssueEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.ConvertedToDiscussionEvent(y => new ConvertedToDiscussionEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Discussion = y.Discussion.Select(discussion => new Discussion
					{
						Number = discussion.Number,
						Title = discussion.Title,
					})
					.SingleOrDefault(),
				})
				.CrossReferencedEvent(y => new CrossReferencedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					IsCrossRepository = y.IsCrossRepository,
					ReferencedAt = y.ReferencedAt,
					WillCloseTarget = y.WillCloseTarget,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Source = y.Source.Select(source => new ReferencedSubject
					{
						Issue = source.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Number = issue.Number,
							Title = issue.Title,

							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = source.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Number = pr.Number,
							Title = pr.Title,

							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),

					Target = y.Target.Select(target => new ReferencedSubject
					{
						Issue = target.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Number = issue.Number,
							Title = issue.Title,

							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = target.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Number = pr.Number,
							Title = pr.Title,

							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),
				})
				.DemilestonedEvent(y => new DemilestonedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					MilestoneTitle = y.MilestoneTitle,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.DisconnectedEvent(y => new DisconnectedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					IsCrossRepository = y.IsCrossRepository,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Source = y.Source.Select(source => new ReferencedSubject
					{
						Issue = source.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = source.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),

					Subject = y.Subject.Select(subject => new ReferencedSubject
					{
						Issue = subject.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
							Repository = issue.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),

						PullRequest = subject.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
							Repository = pr.Repository.Select(repo => new Repository
							{
								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
								Name = repo.Name,
							})
							.SingleOrDefault(),
						})),
					})
					.SingleOrDefault(),
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
						AvatarUrl = author.AvatarUrl(500),
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
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
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
				})
				.LockedEvent(y => new LockedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					LockReason = (LockReason)y.LockReason,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.MarkedAsDuplicateEvent(y => new MarkedAsDuplicateEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Canonical = y.Canonical.Select(canonical => new IssueOrPullRequest
					{
						Issue = canonical.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
						})),

						PullRequest = canonical.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
						})),
					})
					.SingleOrDefault(),

					Duplicate = y.Duplicate.Select(duplicate => new IssueOrPullRequest
					{
						Issue = duplicate.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
						})),

						PullRequest = duplicate.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
						})),
					})
					.SingleOrDefault(),
				})
				.MentionedEvent(y => new MentionedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.MilestonedEvent(y => new MilestonedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					MilestoneTitle = y.MilestoneTitle,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.MovedColumnsInProjectEvent(y => new MovedColumnsInProjectEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.PinnedEvent(y => new PinnedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.ReferencedEvent(y => new ReferencedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					IsCrossRepository = y.IsCrossRepository,
					IsDirectReference = y.IsDirectReference,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
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
						Name = from.Name,

						Owner = from.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),
				})
				.RemovedFromProjectEvent(y => new RemovedFromProjectEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.RenamedTitleEvent(y => new RenamedTitleEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					CurrentTitle = y.CurrentTitle,
					Id = y.Id,
					PreviousTitle = y.PreviousTitle,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.ReopenedEvent(y => new ReopenedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,
					StateReason = (IssueStateReason)y.StateReason,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.SubscribedEvent(y => new SubscribedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.TransferredEvent(y => new TransferredEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					FromRepository = y.FromRepository.Select(from => new Repository
					{
						Owner = from.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Login = owner.Login,
						})
						.SingleOrDefault(),

						Name = from.Name,
					})
					.SingleOrDefault(),
				})
				.UnassignedEvent(y => new UnassignedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Assignee = y.Assignee.Select(assignee => new Assignee
					{
						Bot = assignee.Switch<Bot>(whenBot => whenBot
						.Bot(bot => new Bot
						{
							Login = bot.Login,
						})),
						Mannequin = assignee.Switch<Mannequin>(whenMannequin => whenMannequin
						.Mannequin(mannequin => new Mannequin
						{
							Login = mannequin.Login,
						})),
						Organization = assignee.Switch<Organization>(whenOrganization => whenOrganization
						.Organization(organization => new Organization
						{
							Login = organization.Login,
						})),
						User = assignee.Switch<User>(whenUser => whenUser
						.User(user => new User
						{
							Login = user.Login,
						})),
					})
					.SingleOrDefault(),

					//[Obsolete]
					//User = y.User.Select(user => new User
					//{
					//	Login = user.Login,
					//})
					//.SingleOrDefault(),
				})
				.UnlabeledEvent(y => new UnlabeledEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
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
				})
				.UnlockedEvent(y => new UnlockedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.UnmarkedAsDuplicateEvent(y => new UnmarkedAsDuplicateEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Canonical = y.Canonical.Select(canonical => new IssueOrPullRequest
					{
						Issue = canonical.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
						})),

						PullRequest = canonical.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
						})),
					})
					.SingleOrDefault(),

					Duplicate = y.Duplicate.Select(duplicate => new IssueOrPullRequest
					{
						Issue = duplicate.Switch<Issue>(whenIssue => whenIssue
						.Issue(issue => new Issue
						{
							Title = issue.Title,
						})),

						PullRequest = duplicate.Switch<PullRequest>(whenPr => whenPr
						.PullRequest(pr => new PullRequest
						{
							Title = pr.Title,
						})),
					})
					.SingleOrDefault(),
				})
				.UnpinnedEvent(y => new UnpinnedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.UnsubscribedEvent(y => new UnsubscribedEvent
				{
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),
				})
				.UserBlockedEvent(y => new UserBlockedEvent
				{
					BlockDuration = (UserBlockDuration)y.BlockDuration,
					CreatedAt = y.CreatedAt,
					CreatedAtHumanized = y.CreatedAt.Humanize(null, null),
					Id = y.Id,

					Actor = y.Actor.Select(actor => new Actor
					{
						AvatarUrl = actor.AvatarUrl(500),
						Login = actor.Login,
					})
					.SingleOrDefault(),

					Subject = y.Subject.Select(subject => new User
					{
						Login = subject.Login,
					})
					.SingleOrDefault(),
				})
				))
				.Compile();

			var response = await App.Connection.Run(query);

			return response.ToList();
		}
	}
}
