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
                            AvatarUrl = owner.AvatarUrl(100),
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
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id,
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),

                    ActiveLockReason = (LockReason)x.ActiveLockReason,
                    AuthorAssociation = (CommentAuthorAssociation)x.AuthorAssociation,
                    BodyHTML = x.BodyHTML,
                    Id = x.Id,
                    IncludesCreatedEdit = x.IncludesCreatedEdit,
                    Locked = x.Locked,
                    Number = x.Number,
                    Title = x.Title,
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

                    AnswerChosenAt = x.AnswerChosenAt,
                    CreatedAt = x.CreatedAt,
                    LastEditedAt = x.LastEditedAt,
                    PublishedAt = x.PublishedAt,
                    UpdatedAt = x.UpdatedAt,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
