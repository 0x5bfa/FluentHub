using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Clients
{
    public class UserFollowingClient
    {
        public UserFollowingClient() { _ = new App(); }

        public async Task<List<Models.UserFollowers>> Get(string login)
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

            List<Models.UserFollowers> formattedFollowingList = new List<Models.UserFollowers>();

            var result = await App.Connection.Run(query);

            foreach (var res in result)
            {
                Models.UserFollowers following = new Models.UserFollowers();

                following.AvatarUrl = res.AvatarUrl;
                following.Name = res.Name;
                following.Login = res.Login;
                following.Bio = res.Bio;

                formattedFollowingList.Add(following);
            }

            return formattedFollowingList;
        }
    }
}
