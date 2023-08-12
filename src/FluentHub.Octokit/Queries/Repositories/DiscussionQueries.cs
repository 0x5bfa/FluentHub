namespace FluentHub.Octokit.Queries.Repositories
{
	public class DiscussionQueries
	{
		public async Task<List<Discussion>> GetAllAsync(string owner, string name)
		{
			var query = new Query()
				.Repository(owner: owner, name: name)
				.Discussions(first: 30)
				.Nodes
				.Select(x => new Discussion
				{
					AnswerChosenAt = x.AnswerChosenAt,
					Id = x.Id,
					Locked = x.Locked,
					Number = x.Number,
					Title = x.Title,
					UpdatedAt = x.UpdatedAt,
					UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
					UpvoteCount = x.UpvoteCount,
					Url = x.Url,
					ViewerCanDelete = x.ViewerCanDelete,
					ViewerDidAuthor = x.ViewerDidAuthor,
					ViewerHasUpvoted = x.ViewerHasUpvoted,

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
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response.ToList();
		}

		public async Task<Discussion> GetAsync(string owner, string name, int number)
		{
			var query = new Query()
				.Repository(owner: owner, name: name)
				.Discussion(number)
				.Select(x => new Discussion
				{
					ActiveLockReason = (LockReason)x.ActiveLockReason,
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
					UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
					UpvoteCount = x.UpvoteCount,
					Url = x.Url,
					ViewerCanDelete = x.ViewerCanDelete,
					ViewerCanReact = x.ViewerCanReact,
					ViewerCanSubscribe = x.ViewerCanSubscribe,
					ViewerCanUpdate = x.ViewerCanUpdate,
					ViewerCanUpvote = x.ViewerCanUpvote,
					ViewerDidAuthor = x.ViewerDidAuthor,
					ViewerHasUpvoted = x.ViewerHasUpvoted,
					ViewerSubscription = (SubscriptionState)x.ViewerSubscription,

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

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
