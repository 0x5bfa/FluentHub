namespace FluentHub.Octokit.Queries.Repositories
{
    public class ContentQueries
    {
        public async Task<List<TreeEntry>> GetAllAsync(string name, string owner, string refs, string path)
        {
            var query = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<OctokitGraphQLModel.Tree>()
                .Entries
                .Select(x => new TreeEntry
                {
                    Name = x.Name,
                    Path = x.Path,
                    Type = x.Type,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
