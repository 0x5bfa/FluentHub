using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.OctokitEx
{
    public class PullReviewStatus
    {
        public bool GetLatestReviewStatus(string owner, string name, int number)
        {
            try
            {
                /*
                string token = App.Settings.Get("AccessToken", "");

                var graphQLClient = new GraphQLHttpClient($"{App.GraphQlApiEndPoint}", new NewtonsoftJsonSerializer());

                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

                string query = null;

                List<long> repoIdList = new List<long>();

                query = "query { user(login: \"" + username + "\") {pinnedItems(first: 6) {nodes {... on Repository {nameWithOwner}}}}}";
                var repositoriesRequest = new GraphQLRequest { Query = query };

                var graphQLResponse = await graphQLClient.SendQueryAsync<UserResponseType>(repositoriesRequest);

                foreach (var fullname in graphQLResponse.Data.User.PinnedItems.Nodes)
                {
                    var repoId = await GetRepoIdFromRepoNameAndOwner(fullname.NameWithOwner);
                    repoIdList.Add(repoId);
                }
                */

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
