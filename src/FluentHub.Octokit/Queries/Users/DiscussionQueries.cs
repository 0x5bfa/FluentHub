namespace FluentHub.Octokit.Queries.Users
{
	public class DiscussionQueries
	{
		public async Task<OctokitQueryResult> GetAllAsync(
			string login,
			int? first = null,
			string? after = null,
			int? last = null,
			string? before = null,
			bool? answered = null,
			OctokitGraphQLModel.DiscussionOrder? orderBy = null,
			ID? repositoryId = null)
		{
			var query = new Query()
				.User(login)
				.RepositoryDiscussions(
					first,
					after,
					last,
					before,
					answered,
					orderBy,
					repositoryId)
				.Select(root => new DiscussionConnection
				{
					Edges = root.Edges.Select(x => new DiscussionEdge
					{
						Node = x.Node.Select(x => new Discussion
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
