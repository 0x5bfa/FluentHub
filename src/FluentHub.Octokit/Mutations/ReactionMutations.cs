using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public sealed class ReactionMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public ReactionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<AddReactionPayload> AddAsync(
			AddReactionInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.AddReaction(new(new OctokitGraphQLModel.AddReactionInput
				{
					SubjectId = input.SubjectId,
					Content = (OctokitGraphQLModel.ReactionContent)input.Content,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new AddReactionPayload
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

		public Task<RemoveReactionPayload> RemoveAsync(
			RemoveReactionInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.RemoveReaction(new(new OctokitGraphQLModel.RemoveReactionInput
				{
					SubjectId = input.SubjectId,
					Content = (OctokitGraphQLModel.ReactionContent)input.Content,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new RemoveReactionPayload
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
