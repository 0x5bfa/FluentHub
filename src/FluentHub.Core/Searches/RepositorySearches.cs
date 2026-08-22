// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Clients;

namespace FluentHub.Core.Searches
{
	public class RepositorySearches
	{
		private readonly IGitHubApiClient _gitHub;

		public RepositorySearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Repository>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var request = new OctokitV3.SearchRepositoriesRequest(term);
			var response = await _gitHub.RunRestAsync(
				client => client.Search.SearchRepo(request),
				cancellationToken);

			List<Repository> result = new();

			foreach (var item in response.Items)
			{
				result.Add(new Repository
				{
					Name = item.Name,
					Description = item.Description,
					ForkCount = item.ForksCount,
					StargazerCount = item.StargazersCount,
					UpdatedAt = item.UpdatedAt,
					UpdatedAtHumanized = item.UpdatedAt.ToRelativeTime(),

					Issues = new()
					{
						TotalCount = item.OpenIssuesCount,
					},

					Owner = new RepositoryOwner()
					{
						AvatarUrl = item.Owner.AvatarUrl,
						Login = item.Owner.Login,
					},
				});
			}

			return result;
		}
	}
}
