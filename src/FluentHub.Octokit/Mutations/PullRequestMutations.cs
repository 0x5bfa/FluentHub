using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public sealed class PullRequestMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public PullRequestMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<UpdatePullRequestResult> UpdateAsync(
			UpdatePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.UpdatePullRequest(new(new OctokitGraphQLModel.UpdatePullRequestInput
				{
					PullRequestId = request.PullRequestId,
					BaseRefName = request.BaseRefName,
					Title = request.Title,
					Body = request.Body,
					State = request.State is null
						? null
						: (OctokitGraphQLModel.PullRequestUpdateState)request.State.Value,
					MaintainerCanModify = request.MaintainerCanModify,
					AssigneeIds = request.AssigneeIds,
					MilestoneId = request.MilestoneId,
					LabelIds = request.LabelIds,
					ProjectIds = request.ProjectIds,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new UpdatePullRequestResult
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

		public Task<ClosePullRequestResult> CloseAsync(
			ClosePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.ClosePullRequest(new(new OctokitGraphQLModel.ClosePullRequestInput
				{
					PullRequestId = request.PullRequestId,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new ClosePullRequestResult
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

		public Task<ReopenPullRequestResult> ReopenAsync(
			ReopenPullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.ReopenPullRequest(new(new OctokitGraphQLModel.ReopenPullRequestInput
				{
					PullRequestId = request.PullRequestId,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new ReopenPullRequestResult
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

		public Task<MergePullRequestResult> MergeAsync(
			MergePullRequestRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.MergePullRequest(new(new OctokitGraphQLModel.MergePullRequestInput
				{
					PullRequestId = request.PullRequestId,
					CommitHeadline = request.CommitHeadline,
					CommitBody = request.CommitBody,
					ExpectedHeadOid = request.ExpectedHeadOid,
					MergeMethod = request.MergeMethod is null
						? null
						: (OctokitGraphQLModel.PullRequestMergeMethod)request.MergeMethod.Value,
					AuthorEmail = request.AuthorEmail,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new MergePullRequestResult
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

		public Task<AddCommentResult> AddCommentAsync(
			AddCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.AddComment(new(new OctokitGraphQLModel.AddCommentInput
				{
					SubjectId = request.SubjectId,
					Body = request.Body,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new AddCommentResult
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

		public Task<AddPullRequestReviewResult> AddReviewAsync(
			AddPullRequestReviewRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			if (request.Comments is not null || request.Threads is not null)
				throw new NotSupportedException("Inline review comments are not supported by this mutation wrapper.");

			var mutation = new Mutation()
				.AddPullRequestReview(new(new OctokitGraphQLModel.AddPullRequestReviewInput
				{
					PullRequestId = request.PullRequestId,
					CommitOID = request.CommitOID,
					Body = request.Body,
					Event = request.Event is null
						? null
						: (OctokitGraphQLModel.PullRequestReviewEvent)request.Event.Value,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new AddPullRequestReviewResult
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
