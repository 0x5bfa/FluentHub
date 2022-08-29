namespace FluentHub.Octokit.Queries.Users
{
    public class PinnedItemQueries
    {
        public async Task<List<Repository>> GetAllAsync(string login)
        {
            var query = new Query()
                .User(login)
                .PinnedItems(first: 6)
                .Nodes
                .OfType<OctokitGraphQLModel.Repository>()
                .Select(x => new Repository
                {
                    Name = x.Name,
                    Description = x.Description,
                    StargazerCount = x.StargazerCount,
                    IsFork = x.IsFork,
                    IsInOrganization = x.IsInOrganization,
                    ViewerHasStarred = x.ViewerHasStarred,

                    Owner = x.Owner.Select(owner => new RepositoryOwner
                    {
                        AvatarUrl = owner.AvatarUrl(100),
                        Id = owner.Id,
                        Login = owner.Login,
                    })
                    .Single(),

                    PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
                    {
                        Name = y.Name,
                        Color = y.Color,
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<List<Repository>> GetAllPinnableItems(string login)
        {
            var query = new Query()
                .User(login)
                .PinnableItems(first: 6)
                .Nodes
                .OfType<OctokitGraphQLModel.Repository>()
                .Select(x => new Repository
                {
                    Name = x.Name,
                    Description = x.Description,
                    StargazerCount = x.StargazerCount,
                    IsFork = x.IsFork,
                    IsInOrganization = x.IsInOrganization,
                    ViewerHasStarred = x.ViewerHasStarred,

                    Owner = x.Owner.Select(owner => new RepositoryOwner
                    {
                        AvatarUrl = owner.AvatarUrl(100),
                        Id = owner.Id,
                        Login = owner.Login,
                    })
                    .Single(),

                    PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
                    {
                        Name = y.Name,
                        Color = y.Color,
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
