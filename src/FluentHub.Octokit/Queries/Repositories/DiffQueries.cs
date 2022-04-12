using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class DiffQueries
    {
        public DiffQueries() => new App();

        public async Task GetDiffOfCommit(string owner, string name, string refs)
        {
        }
    }
}
