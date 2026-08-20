// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Searches
{
	public class CodeSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public CodeSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Models.v3.Searches.SearchCode>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var request = new OctokitV3.SearchCodeRequest(term);
			var response = await _gitHub.RunRestAsync(
				client => client.Search.SearchCode(request),
				cancellationToken);

			List<Models.v3.Searches.SearchCode> result = new();

			foreach (var item in response.Items)
			{
				result.Add(new()
				{
					Name = item.Name,
					Path = item.Path,

					Repository = new()
					{
						Name = item.Repository.Name,

						Owner = new RepositoryOwner()
						{
							AvatarUrl = item.Repository.Owner.AvatarUrl,
							Login = item.Repository.Owner.Login,
						}
					},
				});
			}

			return result;
		}
	}
}
