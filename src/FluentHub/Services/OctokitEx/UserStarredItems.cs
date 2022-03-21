using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.Octokit
{
    public class UserStarredItems
    {
        public async Task<List<long>> Get(string username)
        {
            try
            {
                string token = App.Settings.Get("AccessToken", "");

                var graphQLClient = new GraphQLHttpClient($"{App.GraphQlApiEndPoint}", new NewtonsoftJsonSerializer());

                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

                var repositoriesRequest = new GraphQLRequest
                {
                    Query = "query { user(login: \"" + username + "\") {starredRepositories(first: 30) { nodes { nameWithOwner }}}}"
                };

                var graphQLResponse = await graphQLClient.SendQueryAsync<ResponseType>(repositoriesRequest);

                List<long> repoIdList = new List<long>();

                foreach (var fullname in graphQLResponse.Data.User.StarredRepositories.Nodes)
                {
                    var repoId = await GetRepoIdFromRepoNameAndOwner(fullname.NameWithOwner);

                    repoIdList.Add(repoId);
                }

                repoIdList.Reverse();

                return repoIdList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<long> GetRepoIdFromRepoNameAndOwner(string nameWithOwner)
        {
            var items = nameWithOwner.Split("/");

            var repo = await App.Client.Repository.Get(items[0], items[1]);

            return repo.Id;
        }

        public class ResponseType
        {
            public UserNode User { get; set; }

            public class UserNode
            {
                public StarredRepositoriesNode StarredRepositories { get; set; }

                public class StarredRepositoriesNode
                {
                    public List<NodeType> Nodes { get; set; }

                    public class NodeType
                    {
                        public string NameWithOwner { get; set; }
                    }
                }
            }
        }
    }
}
