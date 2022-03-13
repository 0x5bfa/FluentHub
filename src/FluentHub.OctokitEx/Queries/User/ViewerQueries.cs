using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries.User
{
    public class ViewerQueries
    {
        public ViewerQueries() { _ = new App(); }

        public async Task<string> GetLoginName()
        {
            var query = new Query().Viewer.Select(x => x.Login);

            var result = await App.Connection.Run(query);

            return result;
        }
    }
}
