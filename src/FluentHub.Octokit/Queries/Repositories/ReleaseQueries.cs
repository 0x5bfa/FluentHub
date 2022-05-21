namespace FluentHub.Octokit.Queries.Repositories
{
    public class ReleaseQueries
    {
        public ReleaseQueries() => new App();

        public async Task<List<Models.Release>> GetAllAsync(string owner, string name)
        {
            OctokitGraphQLCore.Arg<OctokitGraphQLModel.ReleaseOrder> releaseOrder = new(new OctokitGraphQLModel.ReleaseOrder { Direction = OctokitGraphQLModel.OrderDirection.Desc});

            var query = new Query()
                .Repository(name, owner)
                .Releases(null, null, 20, null, releaseOrder)
                .Nodes
                .Select(x => new Release
                {
                    AuthorLogin = x.Author.Login,
                    AuthorAvatarUrl = x.Author.AvatarUrl(100),
                    DescriptionHTML = x.DescriptionHTML,
                    IsDraft = x.IsDraft,
                    IsLatest = x.IsLatest,
                    IsPrerelease = x.IsPrerelease,
                    Name = x.Name,
                    PublishedAt = x.PublishedAt,
                    PublishedAtHumanized = x.PublishedAt.Humanize(null, null),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
