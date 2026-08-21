using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public sealed class PullRequestMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public PullRequestMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<UpdatePullRequestPayload> UpdateAsync(
			UpdatePullRequestInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.UpdatePullRequest(new(new OctokitGraphQLModel.UpdatePullRequestInput
				{
					PullRequestId = input.PullRequestId,
					BaseRefName = input.BaseRefName,
					Title = input.Title,
					Body = input.Body,
					State = input.State is null
						? null
						: (OctokitGraphQLModel.PullRequestUpdateState)input.State.Value,
					MaintainerCanModify = input.MaintainerCanModify,
					AssigneeIds = input.AssigneeIds,
					MilestoneId = input.MilestoneId,
					LabelIds = input.LabelIds,
					ProjectIds = input.ProjectIds,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new UpdatePullRequestPayload
				{
					ClientMutationId = x.ClientMutationId,
					PullRequest = x.PullRequest.Select(pullRequest => new PullRequest
					{
						Id = pullRequest.Id,
						Body = pullRequest.Body,
						Closed = pullRequest.Closed,
						HeadRefOid = pullRequest.HeadRefOid,
						Mergeable = (MergeableState)pullRequest.Mergeable,
						Merged = pullRequest.Merged,
						State = (PullRequestState)pullRequest.State,
						Title = pullRequest.Title,
						UpdatedAt = pullRequest.UpdatedAt,
						UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = pullRequest.ViewerCanUpdate,
						ViewerCanMergeAsAdmin = pullRequest.ViewerCanMergeAsAdmin,
						ViewerCanReopen = pullRequest.ViewerCanUpdate,
						ViewerCanSubscribe = pullRequest.ViewerCanSubscribe,
						ViewerCanUpdate = pullRequest.ViewerCanUpdate,
						ViewerSubscription = pullRequest.ViewerSubscription == null
							? null
							: (SubscriptionState?)pullRequest.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<ClosePullRequestPayload> CloseAsync(
			ClosePullRequestInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.ClosePullRequest(new(new OctokitGraphQLModel.ClosePullRequestInput
				{
					PullRequestId = input.PullRequestId,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new ClosePullRequestPayload
				{
					ClientMutationId = x.ClientMutationId,
					PullRequest = x.PullRequest.Select(pullRequest => new PullRequest
					{
						Id = pullRequest.Id,
						Body = pullRequest.Body,
						Closed = pullRequest.Closed,
						HeadRefOid = pullRequest.HeadRefOid,
						Mergeable = (MergeableState)pullRequest.Mergeable,
						Merged = pullRequest.Merged,
						State = (PullRequestState)pullRequest.State,
						Title = pullRequest.Title,
						UpdatedAt = pullRequest.UpdatedAt,
						UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = pullRequest.ViewerCanUpdate,
						ViewerCanMergeAsAdmin = pullRequest.ViewerCanMergeAsAdmin,
						ViewerCanReopen = pullRequest.ViewerCanUpdate,
						ViewerCanSubscribe = pullRequest.ViewerCanSubscribe,
						ViewerCanUpdate = pullRequest.ViewerCanUpdate,
						ViewerSubscription = pullRequest.ViewerSubscription == null
							? null
							: (SubscriptionState?)pullRequest.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<ReopenPullRequestPayload> ReopenAsync(
			ReopenPullRequestInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.ReopenPullRequest(new(new OctokitGraphQLModel.ReopenPullRequestInput
				{
					PullRequestId = input.PullRequestId,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new ReopenPullRequestPayload
				{
					ClientMutationId = x.ClientMutationId,
					PullRequest = x.PullRequest.Select(pullRequest => new PullRequest
					{
						Id = pullRequest.Id,
						Body = pullRequest.Body,
						Closed = pullRequest.Closed,
						HeadRefOid = pullRequest.HeadRefOid,
						Mergeable = (MergeableState)pullRequest.Mergeable,
						Merged = pullRequest.Merged,
						State = (PullRequestState)pullRequest.State,
						Title = pullRequest.Title,
						UpdatedAt = pullRequest.UpdatedAt,
						UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = pullRequest.ViewerCanUpdate,
						ViewerCanMergeAsAdmin = pullRequest.ViewerCanMergeAsAdmin,
						ViewerCanReopen = pullRequest.ViewerCanUpdate,
						ViewerCanSubscribe = pullRequest.ViewerCanSubscribe,
						ViewerCanUpdate = pullRequest.ViewerCanUpdate,
						ViewerSubscription = pullRequest.ViewerSubscription == null
							? null
							: (SubscriptionState?)pullRequest.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<MergePullRequestPayload> MergeAsync(
			MergePullRequestInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.MergePullRequest(new(new OctokitGraphQLModel.MergePullRequestInput
				{
					PullRequestId = input.PullRequestId,
					CommitHeadline = input.CommitHeadline,
					CommitBody = input.CommitBody,
					ExpectedHeadOid = input.ExpectedHeadOid,
					MergeMethod = input.MergeMethod is null
						? null
						: (OctokitGraphQLModel.PullRequestMergeMethod)input.MergeMethod.Value,
					AuthorEmail = input.AuthorEmail,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new MergePullRequestPayload
				{
					ClientMutationId = x.ClientMutationId,
					PullRequest = x.PullRequest.Select(pullRequest => new PullRequest
					{
						Id = pullRequest.Id,
						Body = pullRequest.Body,
						Closed = pullRequest.Closed,
						HeadRefOid = pullRequest.HeadRefOid,
						Mergeable = (MergeableState)pullRequest.Mergeable,
						Merged = pullRequest.Merged,
						State = (PullRequestState)pullRequest.State,
						Title = pullRequest.Title,
						UpdatedAt = pullRequest.UpdatedAt,
						UpdatedAtHumanized = pullRequest.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = pullRequest.ViewerCanUpdate,
						ViewerCanMergeAsAdmin = pullRequest.ViewerCanMergeAsAdmin,
						ViewerCanReopen = pullRequest.ViewerCanUpdate,
						ViewerCanSubscribe = pullRequest.ViewerCanSubscribe,
						ViewerCanUpdate = pullRequest.ViewerCanUpdate,
						ViewerSubscription = pullRequest.ViewerSubscription == null
							? null
							: (SubscriptionState?)pullRequest.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<AddCommentPayload> AddCommentAsync(
			AddCommentInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.AddComment(new(new OctokitGraphQLModel.AddCommentInput
				{
					SubjectId = input.SubjectId,
					Body = input.Body,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new AddCommentPayload
				{
					ClientMutationId = x.ClientMutationId,
					CommentEdge = x.CommentEdge.Select(edge => new IssueCommentEdge
					{
						Cursor = edge.Cursor,
						Node = edge.Node.Select(comment => new IssueComment
						{
							AuthorAssociation = (CommentAuthorAssociation)comment.AuthorAssociation,
							Body = comment.Body,
							BodyHTML = comment.BodyHTML,
							CreatedAt = comment.CreatedAt,
							CreatedAtHumanized = comment.CreatedAt.ToRelativeTime(),
							Id = comment.Id,
							LastEditedAt = comment.LastEditedAt,
							UpdatedAt = comment.UpdatedAt,
							UpdatedAtHumanized = comment.UpdatedAt.ToRelativeTime(),
							Url = comment.Url,
							ViewerCanDelete = comment.ViewerCanDelete,
							ViewerCanReact = comment.ViewerCanReact,
							ViewerCanUpdate = comment.ViewerCanUpdate,
							ViewerDidAuthor = comment.ViewerDidAuthor,
							Author = comment.Author.Select(author => new Actor
							{
								AvatarUrl = author.AvatarUrl(500),
								Login = author.Login,
							}).SingleOrDefault(),
						}).SingleOrDefault(),
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<AddPullRequestReviewPayload> AddReviewAsync(
			AddPullRequestReviewInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			if (input.Comments is not null || input.Threads is not null)
				throw new NotSupportedException("Inline review comments are not supported by this mutation wrapper.");

			var mutation = new Mutation()
				.AddPullRequestReview(new(new OctokitGraphQLModel.AddPullRequestReviewInput
				{
					PullRequestId = input.PullRequestId,
					CommitOID = input.CommitOID,
					Body = input.Body,
					Event = input.Event is null
						? null
						: (OctokitGraphQLModel.PullRequestReviewEvent)input.Event.Value,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new AddPullRequestReviewPayload
				{
					ClientMutationId = x.ClientMutationId,
					PullRequestReview = x.PullRequestReview.Select(review => new PullRequestReview
					{
						Body = review.Body,
						CreatedAt = review.CreatedAt,
						Id = review.Id,
						State = (PullRequestReviewState)review.State,
						SubmittedAt = review.SubmittedAt,
						Url = review.Url,
						Author = review.Author.Select(author => new Actor
						{
							AvatarUrl = author.AvatarUrl(500),
							Login = author.Login,
						}).SingleOrDefault(),
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
