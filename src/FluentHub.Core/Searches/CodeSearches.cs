// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Clients;

namespace FluentHub.Core.Searches
{
	public class CodeSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public CodeSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<SearchCode>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var request = new OctokitV3.SearchCodeRequest(term);
			var response = await _gitHub.RunRestAsync(
				client => client.Search.SearchCode(request),
				cancellationToken);

			List<SearchCode> result = new();

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
