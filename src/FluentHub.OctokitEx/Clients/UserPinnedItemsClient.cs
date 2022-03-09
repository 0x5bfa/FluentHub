using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Clients
{
    public class UserPinnedItemsClient
    {
        public UserPinnedItemsClient() => new App();

        public async Task<List<Models.UserFollowers>> Get(string login, bool isUser)
        {
            var userPinnedItemsQuery = new Query()
                    .User(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .Select(x => x.Switch<object>(when => when
                    .Repository(repo => new
                    {
                        repo.Name,
                        owner = repo.Owner.Select(owner => owner.Login),
                        repo.Description,
                        repo.StargazerCount,
                        language = repo.Languages(1, null, null, null, null).Nodes.Select(language => language.Name),
                        //license = repo.LicenseInfo.Select(license => license.Name),
                        //repo.ForkCount,
                        //issueCount = repo.Issues(null, null, null, null, null, null, null, null).TotalCount,
                        //pullCount = repo.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                        //repo.UpdatedAt,
                    })))
                    .Compile();

            var orgPinnedItemsQuery = new Query()
                    .Organization(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .Select(x => x.Switch<object>(when => when
                    .Repository(repo => new
                    {
                        repo.Name,
                        owner = repo.Owner.Select(owner => owner.Login),
                        repo.Description,
                        repo.StargazerCount,
                        language = repo.Languages(1, null, null, null, null).Nodes.Select(language => language.Name),
                        //license = repo.LicenseInfo.Select(license => license.Name),
                        //repo.ForkCount,
                        //issueCount = repo.Issues(null, null, null, null, null, null, null, null).TotalCount,
                        //pullCount = repo.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                        //repo.UpdatedAt,
                    })))
                    .Compile();

            var result = await App.Connection.Run(isUser ? userPinnedItemsQuery: orgPinnedItemsQuery);

            return null;
        }
    }
}
