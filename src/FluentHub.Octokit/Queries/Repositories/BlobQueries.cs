namespace FluentHub.Octokit.Queries.Repositories
{
    public class BlobQueries
    {
        public async Task<(string, long)> GetAsync(string name, string owner, string branch, string path)
        {
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: branch + ":" + path)
                .Cast<OctokitGraphQLModel.Blob>()
                .Select(x => new
                {
                    x.Text,
                    x.ByteSize,
                })
                .Compile();

            var response = await App.Connection.Run(queryToGetFileInfo);

            return (response.Text, response.ByteSize);
        }
    }
}
