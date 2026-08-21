using FluentHub.Core.Clients;

namespace FluentHub.Core.Mutations
{
	public class IssueMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<CreateIssueResult> CreateIssueAsync(
			CreateIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.CreateIssue(new(ToGraphQLInput(request)))
				.Select(x => new CreateIssueResult
				{
					ClientMutationId = x.ClientMutationId,
					Issue = x.Issue.Select(issue => new Issue
					{
						Id = issue.Id,
						Body = issue.Body,
						Closed = issue.Closed,
						Number = issue.Number,
						State = (IssueState)issue.State,
						StateReason = issue.StateReason == null ? null : (IssueStateReason?)issue.StateReason.Value,
						Title = issue.Title,
						UpdatedAt = issue.UpdatedAt,
						UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = issue.ViewerCanUpdate,
						ViewerCanLabel = issue.ViewerCanUpdate,
						ViewerCanReopen = issue.ViewerCanUpdate,
						ViewerCanSubscribe = issue.ViewerCanSubscribe,
						ViewerCanUpdate = issue.ViewerCanUpdate,
						ViewerSubscription = issue.ViewerSubscription == null
							? null
							: (SubscriptionState?)issue.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<UpdateIssueResult> UpdateIssueAsync(
			UpdateIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.UpdateIssue(new(ToGraphQLInput(request)))
				.Select(x => new UpdateIssueResult
				{
					ClientMutationId = x.ClientMutationId,
					Issue = x.Issue.Select(issue => new Issue
					{
						Id = issue.Id,
						Body = issue.Body,
						Closed = issue.Closed,
						Number = issue.Number,
						State = (IssueState)issue.State,
						StateReason = issue.StateReason == null ? null : (IssueStateReason?)issue.StateReason.Value,
						Title = issue.Title,
						UpdatedAt = issue.UpdatedAt,
						UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = issue.ViewerCanUpdate,
						ViewerCanLabel = issue.ViewerCanUpdate,
						ViewerCanReopen = issue.ViewerCanUpdate,
						ViewerCanSubscribe = issue.ViewerCanSubscribe,
						ViewerCanUpdate = issue.ViewerCanUpdate,
						ViewerSubscription = issue.ViewerSubscription == null
							? null
							: (SubscriptionState?)issue.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<CloseIssueResult> CloseIssueAsync(
			CloseIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.CloseIssue(new(ToGraphQLInput(request)))
				.Select(x => new CloseIssueResult
				{
					ClientMutationId = x.ClientMutationId,
					Issue = x.Issue.Select(issue => new Issue
					{
						Id = issue.Id,
						Body = issue.Body,
						Closed = issue.Closed,
						Number = issue.Number,
						State = (IssueState)issue.State,
						StateReason = issue.StateReason == null ? null : (IssueStateReason?)issue.StateReason.Value,
						Title = issue.Title,
						UpdatedAt = issue.UpdatedAt,
						UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = issue.ViewerCanUpdate,
						ViewerCanLabel = issue.ViewerCanUpdate,
						ViewerCanReopen = issue.ViewerCanUpdate,
						ViewerCanSubscribe = issue.ViewerCanSubscribe,
						ViewerCanUpdate = issue.ViewerCanUpdate,
						ViewerSubscription = issue.ViewerSubscription == null
							? null
							: (SubscriptionState?)issue.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<ReopenIssueResult> ReopenIssueAsync(
			ReopenIssueRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.ReopenIssue(new(ToGraphQLInput(request)))
				.Select(x => new ReopenIssueResult
				{
					ClientMutationId = x.ClientMutationId,
					Issue = x.Issue.Select(issue => new Issue
					{
						Id = issue.Id,
						Body = issue.Body,
						Closed = issue.Closed,
						Number = issue.Number,
						State = (IssueState)issue.State,
						StateReason = issue.StateReason == null ? null : (IssueStateReason?)issue.StateReason.Value,
						Title = issue.Title,
						UpdatedAt = issue.UpdatedAt,
						UpdatedAtHumanized = issue.UpdatedAt.ToRelativeTime(),
						ViewerCanClose = issue.ViewerCanUpdate,
						ViewerCanLabel = issue.ViewerCanUpdate,
						ViewerCanReopen = issue.ViewerCanUpdate,
						ViewerCanSubscribe = issue.ViewerCanSubscribe,
						ViewerCanUpdate = issue.ViewerCanUpdate,
						ViewerSubscription = issue.ViewerSubscription == null
							? null
							: (SubscriptionState?)issue.ViewerSubscription.Value,
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
				.AddComment(new(ToGraphQLInput(request)))
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

		public Task<UpdateIssueCommentResult> UpdateIssueCommentAsync(
			UpdateIssueCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.UpdateIssueComment(new(ToGraphQLInput(request)))
				.Select(x => new UpdateIssueCommentResult
				{
					ClientMutationId = x.ClientMutationId,
					IssueComment = x.IssueComment.Select(comment => new IssueComment
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
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<DeleteIssueCommentResult> DeleteIssueCommentAsync(
			DeleteIssueCommentRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.DeleteIssueComment(new(ToGraphQLInput(request)))
				.Select(x => new DeleteIssueCommentResult
				{
					ClientMutationId = x.ClientMutationId,
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		private static OctokitGraphQLModel.CreateIssueInput ToGraphQLInput(CreateIssueRequest request)
			=> new()
			{
				RepositoryId = request.RepositoryId,
				Title = request.Title,
				Body = request.Body,
				AssigneeIds = request.AssigneeIds,
				MilestoneId = request.MilestoneId,
				LabelIds = request.LabelIds,
				ProjectIds = request.ProjectIds,
				IssueTemplate = request.IssueTemplate,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.UpdateIssueInput ToGraphQLInput(UpdateIssueRequest request)
			=> new()
			{
				Id = request.Id,
				Title = request.Title,
				Body = request.Body,
				AssigneeIds = request.AssigneeIds,
				MilestoneId = request.MilestoneId,
				LabelIds = request.LabelIds,
				State = request.State is null ? null : (OctokitGraphQLModel.IssueState)request.State.Value,
				ProjectIds = request.ProjectIds,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.CloseIssueInput ToGraphQLInput(CloseIssueRequest request)
			=> new()
			{
				IssueId = request.IssueId,
				StateReason = ToGraphQLIssueClosedStateReason(request.StateReason),
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.ReopenIssueInput ToGraphQLInput(ReopenIssueRequest request)
			=> new()
			{
				IssueId = request.IssueId,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.AddCommentInput ToGraphQLInput(AddCommentRequest request)
			=> new()
			{
				SubjectId = request.SubjectId,
				Body = request.Body,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.UpdateIssueCommentInput ToGraphQLInput(UpdateIssueCommentRequest request)
			=> new()
			{
				Id = request.Id,
				Body = request.Body,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.DeleteIssueCommentInput ToGraphQLInput(DeleteIssueCommentRequest request)
			=> new()
			{
				Id = request.Id,
				ClientMutationId = request.ClientMutationId,
			};

		private static OctokitGraphQLModel.IssueClosedStateReason? ToGraphQLIssueClosedStateReason(IssueClosedStateReason? stateReason)
			=> stateReason switch
			{
				null => null,
				IssueClosedStateReason.Completed => OctokitGraphQLModel.IssueClosedStateReason.Completed,
				IssueClosedStateReason.NotPlanned => OctokitGraphQLModel.IssueClosedStateReason.NotPlanned,
				_ => throw new NotSupportedException("Duplicate close reason is not supported by the current Octokit.GraphQL package."),
			};
	}
}
