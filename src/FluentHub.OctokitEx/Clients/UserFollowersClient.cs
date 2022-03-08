using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Clients
{
    public class UserFollowersClient
    {
        public UserFollowersClient() { _ = new App(); }

        public async Task<List<Models.UserFollowers>> Get(string login)
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

            List<Models.UserFollowers> formattedFollowersList = new List<Models.UserFollowers>();

            var result = await App.Connection.Run(query);

            foreach(var res in result)
            {
                Models.UserFollowers follower = new Models.UserFollowers();

                follower.AvatarUrl = res.AvatarUrl;
                follower.Name = res.Name;
                follower.Login = res.Login;
                follower.Bio = res.Bio;

                formattedFollowersList.Add(follower);
            }

            return formattedFollowersList;
        }
    }
}
