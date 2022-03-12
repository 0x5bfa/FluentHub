using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries.Repository
{
    public class BlobQueries
    {
        public BlobQueries() => new App();

        public async Task<string> GetBlob(string name, string owner, string branch, string path)
        {
            // Remove slash
            path = path.Remove(0, 1);

            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: branch+ ":" + path)
                .Cast<Blob>().Select(x => x.Text)
                .Compile();

            var response = await App.Connection.Run(queryToGetFileInfo);

            return response;
        }
    }
}
