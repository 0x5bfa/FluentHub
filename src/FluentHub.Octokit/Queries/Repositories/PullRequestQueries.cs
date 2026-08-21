using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class PullRequestQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public PullRequestQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<PullRequest>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			string? baseRefName = null,
			string? headRefName = null,
			IEnumerable<string>? labels = null,
			OctokitGraphQLModel.IssueOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.PullRequestState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
			};

			var query = new Query()
				.Repository(name, owner)
				.PullRequests(
					page.First,
					page.After,
					page.Last,
					page.Before,
					baseRefName,
					headRefName,
					labels is not null ? new OctokitGraphQLCore.Arg<IEnumerable<string>>(labels) : null!,
					orderBy,
					states is not null ? new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>>(states) : null!)
				.Select(connection => new PullRequestConnection
				{
					Edges = connection.Edges.Select(edge => (PullRequestEdge?)new PullRequestEdge
					{
						Node = edge.Node.Select(x => new PullRequest
						{
							BaseRefName = x.BaseRefName,
							Closed = x.Closed,
							HeadRefName = x.HeadRefName,
							IsDraft = x.IsDraft,
							Merged = x.Merged,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Id = owner.Id,
									Login = owner.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							HeadRepository = x.HeadRepository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							Comments = x.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
							{
								TotalCount = comments.TotalCount,
							}).SingleOrDefault(),

							Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
							{
								Nodes = labels.Nodes.Select(y => (Label?)new Label
								{
									Color = y.Color,
									Description = y.Description,
									Name = y.Name,
								}).ToList(),
							}).SingleOrDefault(),

							Reviews = x.Reviews(null, null, 1, null, null, null).Select(reviews => new PullRequestReviewConnection
							{
								Nodes = reviews.Nodes.Select(y => (PullRequestReview?)new PullRequestReview
								{
									State = (PullRequestReviewState)y.State,
								}).ToList().DefaultIfEmpty().ToList(),
							}).SingleOrDefault(),

							Commits = x.Commits(null, null, 1, null).Select(commits => new PullRequestCommitConnection
							{
								Nodes = commits.Nodes.Select(y => (PullRequestCommit?)new PullRequestCommit
								{
									Commit = y.Commit.Select(commit => new Commit
									{
										StatusCheckRollup = commit.StatusCheckRollup.Select(rollup => new StatusCheckRollup
										{
											State = (StatusState)rollup.State,
										}).SingleOrDefault(),
									}).SingleOrDefault(),
								}).ToList().DefaultIfEmpty().ToList(),
							}).SingleOrDefault(),
						}).Single(),
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<PullRequest>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<PullRequest> GetAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.PullRequest(number)
				.Select(x => new PullRequest
				{
					Additions = x.Additions,
					BaseRefName = x.BaseRefName,
					Body = x.Body,
					ChangedFiles = x.ChangedFiles,
					Closed = x.Closed,
					Deletions = x.Deletions,
					HeadRefName = x.HeadRefName,
					HeadRefOid = x.HeadRefOid,
					Id = x.Id,
					IsDraft = x.IsDraft,
					Mergeable = (MergeableState)x.Mergeable,
					Merged = x.Merged,
					Number = x.Number,
					State = (PullRequestState)x.State,
					Title = x.Title,
					UpdatedAt = x.UpdatedAt,
					UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),
					ViewerCanClose = x.ViewerCanUpdate,
					ViewerCanMergeAsAdmin = x.ViewerCanMergeAsAdmin,
					ViewerCanReopen = x.ViewerCanUpdate,
					ViewerCanSubscribe = x.ViewerCanSubscribe,
					ViewerCanUpdate = x.ViewerCanUpdate,
					ViewerSubscription = x.ViewerSubscription == null
						? null
						: (SubscriptionState?)x.ViewerSubscription.Value,

					Assignees = x.Assignees(6, null, null, null).Select(assignees => new UserConnection
					{
						Nodes = assignees.Nodes.Select(y => (User?)new User
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

					Commits = x.Commits(null, null, 1, null).Select(commits => new PullRequestCommitConnection
					{
						TotalCount = commits.TotalCount,

						Nodes = commits.Nodes.Select(y => (PullRequestCommit?)new PullRequestCommit
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
							AvatarUrl = owner.AvatarUrl(500),
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),

					Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
					{
						Nodes = labels.Nodes.Select(y => (Label?)new Label
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
						Nodes = latestReviews.Nodes.Select(latestReview => (PullRequestReview?)new PullRequestReview
						{
							Author = latestReview.Author.Select(author => new Actor
							{
								AvatarUrl = author.AvatarUrl(500),
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
						Nodes = participants.Nodes.Select(y => (User?)new User
						{
							AvatarUrl = y.AvatarUrl(500),
							Login = y.Login,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					ProjectCards = x.ProjectCards(6, null, null, null, null).Select(projects => new ProjectCardConnection
					{
						Nodes = projects.Nodes.Select(y => (ProjectCard?)new ProjectCard
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

					ReviewRequests = x.ReviewRequests(15, null, null, null).Select(reviewRequests => new ReviewRequestConnection
					{
						Nodes = reviewRequests.Nodes.Select(reviewRequest => (ReviewRequest?)new ReviewRequest
						{
							RequestedReviewer = reviewRequest.RequestedReviewer.Select(requestedReviewer => new RequestedReviewer
							{
								User = requestedReviewer.Switch<User>(whenUser => whenUser
								.User(user => new User
								{
									AvatarUrl = user.AvatarUrl(500),
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
						Nodes = reviews.Nodes.Select(y => (PullRequestReview?)new PullRequestReview
						{
							State = (PullRequestReviewState)y.State,
						})
						.ToList().DefaultIfEmpty().ToList(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}

		public async Task<IssueComment> GetBodyAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
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
					CreatedAtHumanized = x.CreatedAt.ToRelativeTime(),
					Id = x.Id,
					LastEditedAt = x.LastEditedAt,
					UpdatedAt = x.UpdatedAt,
					UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),
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
						Nodes = reactions.Nodes.Select(reaction => (Reaction?)new Reaction
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

					ReactionGroups = x.ReactionGroups.Select(group => new ReactionGroup
					{
						Content = (ReactionContent)group.Content,
						ViewerHasReacted = group.ViewerHasReacted,
						Reactors = group.Reactors(null, null, null, null).Select(reactors => new ReactorConnection
						{
							TotalCount = reactors.TotalCount,
						}).SingleOrDefault(),
					}).ToList(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
