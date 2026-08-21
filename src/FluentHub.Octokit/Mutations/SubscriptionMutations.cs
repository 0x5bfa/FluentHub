using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Mutations
{
	public sealed class SubscriptionMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public SubscriptionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<UpdateSubscriptionPayload> UpdateAsync(
			UpdateSubscriptionInput input,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(input);

			var mutation = new Mutation()
				.UpdateSubscription(new(new OctokitGraphQLModel.UpdateSubscriptionInput
				{
					SubscribableId = input.SubscribableId,
					State = (OctokitGraphQLModel.SubscriptionState)input.State,
					ClientMutationId = input.ClientMutationId,
				}))
				.Select(x => new UpdateSubscriptionPayload
				{
					ClientMutationId = x.ClientMutationId,
					Subscribable = x.Subscribable.Select(subject => new Subscribable
					{
						Id = subject.Id,
						ViewerCanSubscribe = subject.ViewerCanSubscribe,
						ViewerSubscription = subject.ViewerSubscription == null
							? null
							: (SubscriptionState?)subject.ViewerSubscription.Value,
					}).SingleOrDefault(),
				})
				.Compile();

			return _gitHub.RunGraphQLAsync(mutation, cancellationToken);
		}
	}
}
