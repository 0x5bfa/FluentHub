using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public partial class RemoveStarMutation
	{
		[GeneratedGraphQLOperation<GraphQLResult<RemoveStarResult>>]
		private const string RemoveStar = """
			mutation RemoveStar($input: RemoveStarInput!) {
			  result: removeStar(input: $input) {
			    clientMutationId
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public RemoveStarMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<RemoveStarResult> ExecuteAsync(
			ID starrableRepoId,
			CancellationToken cancellationToken = default)
		{
			return ExecuteCoreAsync(starrableRepoId, cancellationToken);
		}

		private async Task<RemoveStarResult> ExecuteCoreAsync(ID starrableRepoId, CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				RemoveStarOperation,
				GitHubGraphQLJsonContext.Default.GraphQLResultRemoveStarResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("starrableId", starrableRepoId.Value);
					writer.WriteEndObject();
				},
				cancellationToken);

			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete remove-star response.");
		}
	}
}
