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
        public StatusQueries() { _ = new App(); }

        public async Task<Octokit.GraphQL.Model.UserStatus> Get(string login)
        {
            var query = new Query().User(login).Status;

            var result = await App.Connection.Run(query);
            return result;
        }
    }
}
