using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class ViewerQueries
    {
        public ViewerQueries() => new App();

        public async Task<string> GetLoginName()
        {
            try
            {
                var query = new Query().Viewer.Select(x => x.Login).Compile();

                var result = await App.Connection.Run(query);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }
    }
}
