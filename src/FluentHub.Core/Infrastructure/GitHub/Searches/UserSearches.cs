// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Searches
{
	public class UserSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public UserSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<User>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Search.SearchUsersAsync(term, token),
				cancellationToken);

			List<User> result = new();

			foreach (var item in response.Items)
			{
				result.Add(new User
				{
					AvatarUrl = item.AvatarUrl ?? string.Empty,
					Bio = item.Bio,
					Location = item.Location,
					Login = item.Login,
					Name = item.Name,
				});
			}

			return result;
		}
	}
}
