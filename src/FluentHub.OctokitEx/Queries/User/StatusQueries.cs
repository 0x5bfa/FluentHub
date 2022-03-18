using Octokit.GraphQL.Model;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries
{
    public class StatusQueries
    {
        public StatusQueries() => new App();

        public async Task<UserStatus> Get(string login)
        {
            var query = new Query().User(login).Status;

            var result = await App.Connection.Run(query);
            return result;
        }
    }
}
