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
			var request = new OctokitV3.SearchUsersRequest(term);
			var response = await _gitHub.RunRestAsync(
				client => client.Search.SearchUsers(request),
				cancellationToken);

			List<User> result = new();

			foreach (var item in response.Items)
			{
				result.Add(new User
				{
					AvatarUrl = item.AvatarUrl,
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
