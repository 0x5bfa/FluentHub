using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public class RemoveStarMutation
	{
		private readonly IGitHubApiClient _gitHub;

		public RemoveStarMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<RemoveStarResult> ExecuteAsync(
			ID starrableRepoId,
			CancellationToken cancellationToken = default)
		{
			var mutation = new Mutation()
				.RemoveStar(new(new OctokitGraphQLModel.RemoveStarInput
				{
					StarrableId = starrableRepoId,
				}))
				.Select(x => new RemoveStarResult
				{
					ClientMutationId = x.ClientMutationId,
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
