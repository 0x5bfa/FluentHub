using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries
{
    public class FollowingQueries
    {
        public FollowingQueries() => new App();

        public async Task<List<Models.UserBlockItem>> GetOverview(string login)
        {
            var query = new Query()
                    .User(login)
                    .Following(first: 30)
                    .Nodes
                    .Select(x => new
                    {
                        AvatarUrl = x.AvatarUrl(100),
                        x.Name,
                        x.Bio,
                        x.Login,
                    })
                    .Compile();

            List<Models.UserBlockItem> formattedFollowingList = new();

            var result = await App.Connection.Run(query);

            foreach (var res in result)
            {
                Models.UserBlockItem item = new();

                item.AvatarUrl = res.AvatarUrl;
                item.Name = res.Name;
                item.Login = res.Login;
                item.Bio = res.Bio;

                formattedFollowingList.Add(item);
            }

            return formattedFollowingList;
        }
    }
}
