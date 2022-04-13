using Humanizer;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class DiffQueries
    {
        public DiffQueries() => new App();

        public async Task GetAllAsync(string owner, string name, string refs)
        {
            var commits = await App.Client.Repository.Commit.Get(owner, name, refs);
        }
    }
}
