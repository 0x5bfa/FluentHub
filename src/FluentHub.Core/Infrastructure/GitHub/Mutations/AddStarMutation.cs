using System.IO;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Mutations
{
	public class AddStarMutation
	{
		private const string AddStar = """
			mutation AddStar($input: AddStarInput!) {
			  result: addStar(input: $input) {
			    clientMutationId
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public AddStarMutation(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<AddStarResult> ExecuteAsync(
			ID starrableRepoId,
			CancellationToken cancellationToken = default)
		{
			return ExecuteCoreAsync(starrableRepoId, cancellationToken);
		}

		private async Task<AddStarResult> ExecuteCoreAsync(ID starrableRepoId, CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				AddStar,
				GitHubGraphQLJsonContext.Default.GraphQLResultAddStarResult,
				writer =>
				{
					writer.WriteStartObject("input");
					writer.WriteString("starrableId", starrableRepoId.Value);
					writer.WriteEndObject();
				},
				cancellationToken);

			return response.Result
				?? throw new InvalidDataException("GitHub returned an incomplete add-star response.");
		}
	}
}
