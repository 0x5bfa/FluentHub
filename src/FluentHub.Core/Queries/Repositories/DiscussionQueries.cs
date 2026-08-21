using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Repositories
{
	public class DiscussionQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public DiscussionQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Discussion>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			ID? categoryId = null,
			OctokitGraphQLModel.DiscussionOrder? orderBy = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			var query = new Query()
				.Repository(owner: owner, name: name)
				.Discussions(
					first: page.First,
					after: page.After,
					last: page.Last,
					before: page.Before,
					categoryId: categoryId,
					orderBy: orderBy)
				.Select(connection => new DiscussionConnection
				{
					Edges = connection.Edges.Select(edge => (DiscussionEdge?)new DiscussionEdge
					{
						Node = edge.Node.Select(x => new Discussion
						{
							AnswerChosenAt = x.AnswerChosenAt,
							Id = x.Id,
							Locked = x.Locked,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),
							UpvoteCount = x.UpvoteCount,
							Url = x.Url,
							ViewerCanDelete = x.ViewerCanDelete,
							ViewerDidAuthor = x.ViewerDidAuthor,
							ViewerHasUpvoted = x.ViewerHasUpvoted,

							Category = x.Category.Select(category => new DiscussionCategory
							{
								Emoji = category.Emoji,
								Id = category.Id,
							}).Single(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Id = owner.Id,
									Login = owner.Login,
								}).Single(),
							}).Single(),
						}).Single(),
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

		public async Task<Discussion> GetAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(owner: owner, name: name)
				.Discussion(number)
				.Select(x => new Discussion
				{
					ActiveLockReason = (LockReason?)x.ActiveLockReason,
					AnswerChosenAt = x.AnswerChosenAt,
					AuthorAssociation = (CommentAuthorAssociation)x.AuthorAssociation,
					BodyHTML = x.BodyHTML,
					CreatedAt = x.CreatedAt,
					Id = x.Id,
					IncludesCreatedEdit = x.IncludesCreatedEdit,
					LastEditedAt = x.LastEditedAt,
					Locked = x.Locked,
					Number = x.Number,
					PublishedAt = x.PublishedAt,
					Title = x.Title,
					UpdatedAt = x.UpdatedAt,
					UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),
					UpvoteCount = x.UpvoteCount,
					Url = x.Url,
					ViewerCanDelete = x.ViewerCanDelete,
					ViewerCanReact = x.ViewerCanReact,
					ViewerCanSubscribe = x.ViewerCanSubscribe,
					ViewerCanUpdate = x.ViewerCanUpdate,
					ViewerCanUpvote = x.ViewerCanUpvote,
					ViewerDidAuthor = x.ViewerDidAuthor,
					ViewerHasUpvoted = x.ViewerHasUpvoted,
					ViewerSubscription = (SubscriptionState?)x.ViewerSubscription,

					Category = x.Category.Select(category => new DiscussionCategory
					{
						CreatedAt = category.CreatedAt,
						Description = category.Description,
						Emoji = category.Emoji,
						Id = category.Id,
						Name = category.Name,
						UpdatedAt = category.UpdatedAt,
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
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}
	}
}
