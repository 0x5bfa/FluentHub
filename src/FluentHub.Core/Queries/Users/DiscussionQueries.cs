using FluentHub.Core.Clients;
using FluentHub.Core.Queries.Discussions;

namespace FluentHub.Core.Queries.Users
{
	public class DiscussionQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public DiscussionQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<PageResult<Discussion>> GetPageAsync(
			string login,
			PageRequest page,
			DiscussionListFilters filters,
			CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetAuthorPageAsync(
				login,
				page,
				filters,
				cancellationToken);

		public Task<IReadOnlyList<string>> GetLabelNamesAsync(
			string login,
			CancellationToken cancellationToken = default)
			=> new DiscussionSearchQueries(_gitHub).GetAuthorLabelNamesAsync(
				login,
				cancellationToken);

		public async Task<PageResult<Discussion>> GetPageAsync(
			string login,
			PageRequest page,
			bool? answered = null,
			OctokitGraphQLModel.DiscussionOrder? orderBy = null,
			ID? repositoryId = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.User(login)
				.RepositoryDiscussions(
					page.First,
					page.After,
					page.Last,
					page.Before,
					answered,
					orderBy,
					repositoryId)
				.Select(connection => new DiscussionConnection
				{
					Edges = connection.Edges.Select(edge => (DiscussionEdge?)new DiscussionEdge
					{
						Node = edge.Node.Select(x => new Discussion
						{
							Category = x.Category.Select(category => new DiscussionCategory
							{
								Emoji = category.Emoji,
								Id = category.Id,
							})
							.Single(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Id = owner.Id,
									Login = owner.Login,
								})
								.Single(),
							})
							.Single(),

							Id = x.Id,
							Locked = x.Locked,
							Number = x.Number,
							Title = x.Title,
							UpvoteCount = x.UpvoteCount,
							Url = x.Url,
							ViewerCanDelete = x.ViewerCanDelete,
							ViewerDidAuthor = x.ViewerDidAuthor,
							ViewerHasUpvoted = x.ViewerHasUpvoted,
							AnswerChosenAt = x.AnswerChosenAt,
							UpdatedAt = x.UpdatedAt,
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

			return new PageResult<Discussion>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
