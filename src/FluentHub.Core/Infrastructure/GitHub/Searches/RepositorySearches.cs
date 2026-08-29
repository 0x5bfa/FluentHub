// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Searches
{
	public class RepositorySearches
	{
		private readonly IGitHubApiClient _gitHub;

		public RepositorySearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Repository>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Search.SearchRepositoriesAsync(term, token),
				cancellationToken);

			List<Repository> result = new();

			foreach (var item in response.Items)
			{
				if (item.Owner is not { } owner)
					continue;

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
						AvatarUrl = owner.AvatarUrl ?? string.Empty,
						Login = owner.Login,
					},
				});
			}

			return result;
		}
	}
}
