// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Searches
{
	public class IssueSearches
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueSearches(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<List<Issue>> GetAllAsync(string term, CancellationToken cancellationToken = default)
		{
			var request = new OctokitV3.SearchIssuesRequest(term);
			var response = await _gitHub.RunRestAsync(
				client => client.Search.SearchIssues(request),
				cancellationToken);

			List<Issue> result = new();

			foreach (var item in response.Items)
			{
				var indivisual = new Issue
				{
					Closed = item.ClosedAt != null,
					CreatedAt = item.CreatedAt,
					Title = item.Title,
					Number = item.Number,

					Author = new Actor()
					{
						AvatarUrl = item.User.AvatarUrl,
						Login = item.User.Login,
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
						Color = label.Color,
						Name = label.Name,
					});
				}

				result.Add(indivisual);
			}

			return result;
		}
	}
}
