﻿// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Searches
{
	public class UserSearches
	{
		public async Task<List<User>> GetAllAsync(string term)
		{
			var request = new OctokitV3.SearchUsersRequest(term);
			var response = await App.Client.Search.SearchUsers(request);

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
