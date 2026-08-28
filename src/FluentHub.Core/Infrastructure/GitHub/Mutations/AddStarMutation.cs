using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public class AddStarMutation
	{
		private readonly IGitHubApiClient _gitHub;

		public AddStarMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<AddStarResult> ExecuteAsync(
			ID starrableRepoId,
			CancellationToken cancellationToken = default)
		{
			var mutation = new Mutation()
				.AddStar(new(new OctokitGraphQLModel.AddStarInput
				{
					StarrableId = starrableRepoId,
				}))
				.Select(x => new AddStarResult
				{
					ClientMutationId = x.ClientMutationId,
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
