using Octokit.GraphQL.Core;

using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Users
{
	public class IssueQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Issue>> GetPageAsync(
			string login,
			PageRequest page,
			OctokitGraphQLModel.IssueFilters? filterBy = null,
			IEnumerable<string>? labels = null,
			OctokitGraphQLModel.IssueOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.IssueState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
			};

			var query = new Query()
				.User(login)
				.Issues(page.First, page.After, page.Last, page.Before, filterBy, labels is null ? null! : new Arg<IEnumerable<string>>(labels), orderBy, states is null ? null! : new Arg<IEnumerable<OctokitGraphQLModel.IssueState>>(states))
				.Select(connection => new IssueConnection
				{
					Edges = connection.Edges.Select(edge => (IssueEdge?)new IssueEdge
					{
						Node = edge.Node.Select(x => new Issue
						{
							Closed = x.Closed,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Id = owner.Id,
									Login = owner.Login,
								})
								.SingleOrDefault(),
							})
							.SingleOrDefault(),

							Comments = x.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
							{
								TotalCount = comments.TotalCount,
							})
							.SingleOrDefault(),

							Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
							{
								Nodes = labels.Nodes.Select(y => (Label?)new Label
								{
									Color = y.Color,
									Description = y.Description,
									Name = y.Name,
								})
								.ToList(),
							})
							.SingleOrDefault(),
						}).Single()
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Issue>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
