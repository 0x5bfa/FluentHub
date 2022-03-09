using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries
{
    public class FollowersQueries
    {
        public FollowersQueries() => new App();

        public async Task<List<Models.UserBlockItem>> Get(string login)
        {
            var query = new Query()
                    .User(login)
                    .Followers(first: 30)
                    .Nodes
                    .Select(x => new
                    {
                        AvatarUrl = x.AvatarUrl(100),
                        x.Name,
                        x.Bio,
                        x.Login,
                    })
                    .Compile();

            List<Models.UserBlockItem> formattedFollowersList = new();

            var result = await App.Connection.Run(query);

            foreach(var res in result)
            {
                Models.UserBlockItem item = new();

                item.AvatarUrl = res.AvatarUrl;
                item.Name = res.Name;
                item.Login = res.Login;
                item.Bio = res.Bio;

                formattedFollowersList.Add(item);
            }

            return formattedFollowersList;
        }
    }
}
