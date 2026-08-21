using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public class IssueMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<CreateIssuePayload> CreateIssueAsync(
			CreateIssueInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.CreateIssue(new(ToGraphQLInput(input)))
				.Select(x => new CreateIssuePayload
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

		public Task<UpdateIssuePayload> UpdateIssueAsync(
			UpdateIssueInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.UpdateIssue(new(ToGraphQLInput(input)))
				.Select(x => new UpdateIssuePayload
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

		public Task<CloseIssuePayload> CloseIssueAsync(
			CloseIssueInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.CloseIssue(new(ToGraphQLInput(input)))
				.Select(x => new CloseIssuePayload
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

		public Task<ReopenIssuePayload> ReopenIssueAsync(
			ReopenIssueInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.ReopenIssue(new(ToGraphQLInput(input)))
				.Select(x => new ReopenIssuePayload
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

		public Task<AddCommentPayload> AddCommentAsync(
			AddCommentInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.AddComment(new(ToGraphQLInput(input)))
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

		public Task<UpdateIssueCommentPayload> UpdateIssueCommentAsync(
			UpdateIssueCommentInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.UpdateIssueComment(new(ToGraphQLInput(input)))
				.Select(x => new UpdateIssueCommentPayload
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

		public Task<DeleteIssueCommentPayload> DeleteIssueCommentAsync(
			DeleteIssueCommentInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.DeleteIssueComment(new(ToGraphQLInput(input)))
				.Select(x => new DeleteIssueCommentPayload
				{
					ClientMutationId = x.ClientMutationId,
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		private static OctokitGraphQLModel.CreateIssueInput ToGraphQLInput(CreateIssueInput input)
			=> new()
			{
				RepositoryId = input.RepositoryId,
				Title = input.Title,
				Body = input.Body,
				AssigneeIds = input.AssigneeIds,
				MilestoneId = input.MilestoneId,
				LabelIds = input.LabelIds,
				ProjectIds = input.ProjectIds,
				IssueTemplate = input.IssueTemplate,
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.UpdateIssueInput ToGraphQLInput(UpdateIssueInput input)
			=> new()
			{
				Id = input.Id,
				Title = input.Title,
				Body = input.Body,
				AssigneeIds = input.AssigneeIds,
				MilestoneId = input.MilestoneId,
				LabelIds = input.LabelIds,
				State = input.State is null ? null : (OctokitGraphQLModel.IssueState)input.State.Value,
				ProjectIds = input.ProjectIds,
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.CloseIssueInput ToGraphQLInput(CloseIssueInput input)
			=> new()
			{
				IssueId = input.IssueId,
				StateReason = ToGraphQLIssueClosedStateReason(input.StateReason),
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.ReopenIssueInput ToGraphQLInput(ReopenIssueInput input)
			=> new()
			{
				IssueId = input.IssueId,
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.AddCommentInput ToGraphQLInput(AddCommentInput input)
			=> new()
			{
				SubjectId = input.SubjectId,
				Body = input.Body,
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.UpdateIssueCommentInput ToGraphQLInput(UpdateIssueCommentInput input)
			=> new()
			{
				Id = input.Id,
				Body = input.Body,
				ClientMutationId = input.ClientMutationId,
			};

		private static OctokitGraphQLModel.DeleteIssueCommentInput ToGraphQLInput(DeleteIssueCommentInput input)
			=> new()
			{
				Id = input.Id,
				ClientMutationId = input.ClientMutationId,
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
