using FluentHub.Core.Clients;

namespace FluentHub.Core.Mutations
{
	public sealed class SubscriptionMutations
	{
		private readonly IGitHubApiClient _gitHub;

		public SubscriptionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<UpdateSubscriptionResult> UpdateAsync(
			UpdateSubscriptionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var mutation = new Mutation()
				.UpdateSubscription(new(new OctokitGraphQLModel.UpdateSubscriptionInput
				{
					SubscribableId = request.SubscribableId,
					State = (OctokitGraphQLModel.SubscriptionState)request.State,
					ClientMutationId = request.ClientMutationId,
				}))
				.Select(x => new UpdateSubscriptionResult
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
