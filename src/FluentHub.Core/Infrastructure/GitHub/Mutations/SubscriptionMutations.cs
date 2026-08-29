using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public sealed class SubscriptionMutations
	{
		private const string UpdateSubscription = """
			mutation UpdateSubscription($input: UpdateSubscriptionInput!) {
			  result: updateSubscription(input: $input) {
			    clientMutationId
			    subscribable {
			      id
			      viewerCanSubscribe
			      viewerSubscription
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public SubscriptionMutations(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public async Task<UpdateSubscriptionResult> UpdateAsync(
			UpdateSubscriptionRequest request,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(request);

			var response = await _gitHub.RunGraphQLAsync(
				UpdateSubscription,
				GitHubGraphQLJsonContext.Default.GraphQLResultUpdateSubscriptionResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("subscribableId", request.SubscribableId.Value);
					writer.WriteString("state", request.State switch
					{
						SubscriptionState.Unsubscribed => "UNSUBSCRIBED",
						SubscriptionState.Subscribed => "SUBSCRIBED",
						SubscriptionState.Ignored => "IGNORED",
						_ => throw new ArgumentOutOfRangeException(nameof(request), request.State, "Unknown subscription state."),
					});
					writer.WriteString("clientMutationId", request.ClientMutationId);
					writer.WriteEndObject();
				},
				cancellationToken);

			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete update-subscription response.");
		}
	}
}
