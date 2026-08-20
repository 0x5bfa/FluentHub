using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public class RemoveStarMutation
	{
		private readonly IGitHubApiClient _gitHub;

		public RemoveStarMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<RemoveStarPayload> ExecuteAsync(
			ID starrableRepoId,
			CancellationToken cancellationToken = default)
		{
			var mutation = new Mutation()
				.RemoveStar(new(new OctokitGraphQLModel.RemoveStarInput
				{
					StarrableId = starrableRepoId,
				}))
				.Select(x => new RemoveStarPayload
				{
					ClientMutationId = x.ClientMutationId,
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
