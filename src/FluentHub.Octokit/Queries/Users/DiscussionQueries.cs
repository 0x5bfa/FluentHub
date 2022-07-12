namespace FluentHub.Octokit.Queries.Users
{
    public class DiscussionQueries
    {
        public async Task<List<Discussion>> GetAllAsync(string login)
        {
            var query = new Query()
                .User(login)
                .RepositoryDiscussions(first: 30)
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
    }
}
