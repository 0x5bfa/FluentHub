using Octokit.GraphQL.Core;

namespace FluentHub.Octokit.Queries.Users
{
	public class IssueQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string login,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			OctokitGraphQLModel.IssueFilters? filterBy = null,
			IEnumerable<string>? labels = null,
			OctokitGraphQLModel.IssueOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.IssueState>? states = null)
		{
			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
			};

			var query = new Query()
				.User(login)
				.Issues(first, after, last, before, filterBy, labels is null ? null : new Arg<IEnumerable<string>>(labels), orderBy, states is null ? null : new Arg<IEnumerable<OctokitGraphQLModel.IssueState>>(states))
				.Select(root => new IssueConnection
				{
					Edges = root.Edges.Select(x => new IssueEdge
					{
						Node = x.Node.Select(x => new Issue
						{
							Closed = x.Closed,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

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
								Nodes = labels.Nodes.Select(y => new Label
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
						EndCursor = root.PageInfo.EndCursor,
						HasNextPage = root.PageInfo.HasNextPage,
						HasPreviousPage = root.PageInfo.HasPreviousPage,
						StartCursor = root.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await App.Connection.Run(query);

			var result = new OctokitQueryResult()
			{
				PageInfo = response.PageInfo,
				Response = response.Edges.Select(x => x.Node).ToList(),
			};

			return result;
		}
	}
}
