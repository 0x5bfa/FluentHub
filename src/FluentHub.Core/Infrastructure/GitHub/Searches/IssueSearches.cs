// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Clients;

namespace FluentHub.Core.Infrastructure.GitHub.Searches
{
	public class IssueSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Issue>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Search.SearchIssuesAsync(term, token),
				cancellationToken);

			List<Issue> result = new();

			foreach (var item in response.Items)
			{
				if (item.User is not { } author)
					continue;

				var indivisual = new Issue
				{
					Closed = item.ClosedAt != null,
					CreatedAt = item.CreatedAt,
					Title = item.Title ?? string.Empty,
					Number = item.Number,

					Author = new Actor()
					{
						AvatarUrl = author.AvatarUrl ?? string.Empty,
						Login = author.Login,
					},

					Comments = new()
					{
						TotalCount = item.Comments,
					},

					Labels = new()
					{
						Nodes = new(),
					},

					//Repository = new()
					//{
					//	Name = item.Repository.Name,

					//	Owner = new RepositoryOwner()
					//	{
					//		AvatarUrl = item.Repository.Owner.AvatarUrl,
					//		Login = item.Repository.Owner.Login,
					//	}
					//},
				};

				foreach (var label in item.Labels)
				{
					indivisual.Labels.Nodes.Add(new Label()
					{
						Color = label.Color ?? string.Empty,
						Name = label.Name ?? string.Empty,
					});
				}

				result.Add(indivisual);
			}

			return result;
		}
	}
}
