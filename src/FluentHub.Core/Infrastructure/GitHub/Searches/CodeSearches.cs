// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Searches
{
	public class CodeSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public CodeSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<SearchCode>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Search.SearchCodeAsync(term, token),
				cancellationToken);

			List<SearchCode> result = new();

			foreach (var item in response.Items)
			{
				if (item.Repository?.Owner is not { } owner)
					continue;

				result.Add(new()
				{
					Name = item.Name ?? string.Empty,
					Path = item.Path ?? string.Empty,

					Repository = new()
					{
						Name = item.Repository.Name,

						Owner = new RepositoryOwner()
						{
							AvatarUrl = owner.AvatarUrl ?? string.Empty,
							Login = owner.Login,
						}
					},
				});
			}

			return result;
		}
	}
}
