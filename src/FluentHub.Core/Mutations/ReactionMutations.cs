using FluentHub.Core.Clients;

namespace FluentHub.Core.Mutations
{
	public sealed class ReactionMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public ReactionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<AddReactionResult> AddAsync(
			AddReactionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.AddReaction(new(new OctokitGraphQLModel.AddReactionInput
				{
					SubjectId = request.SubjectId,
					Content = (OctokitGraphQLModel.ReactionContent)request.Content,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new AddReactionResult
				{
					ClientMutationId = x.ClientMutationId,
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

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}

		public Task<RemoveReactionResult> RemoveAsync(
			RemoveReactionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.RemoveReaction(new(new OctokitGraphQLModel.RemoveReactionInput
				{
					SubjectId = request.SubjectId,
					Content = (OctokitGraphQLModel.ReactionContent)request.Content,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new RemoveReactionResult
				{
					ClientMutationId = x.ClientMutationId,
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

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
