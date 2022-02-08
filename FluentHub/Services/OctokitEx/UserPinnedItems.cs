using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.OctokitEx
{
    public class UserPinnedItems
    {
        public async Task<List<long>> Get(string username, bool isUser)
        {
            try
            {
                string token = App.Settings.Get("AccessToken", "");

                var graphQLClient = new GraphQLHttpClient($"{App.GraphQlApiEndPoint}", new NewtonsoftJsonSerializer());

                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

                string query = null;

                List<long> repoIdList = new List<long>();

                if (isUser)
                {
                    query = "query { user(login: \"" + username + "\") {pinnedItems(first: 6) {nodes {... on Repository {nameWithOwner}}}}}";
                    var repositoriesRequest = new GraphQLRequest { Query = query };

                    var graphQLResponse = await graphQLClient.SendQueryAsync<UserResponseType>(repositoriesRequest);

                    foreach (var fullname in graphQLResponse.Data.User.PinnedItems.Nodes)
                    {
                        var repoId = await GetRepoIdFromRepoNameAndOwner(fullname.NameWithOwner);
                        repoIdList.Add(repoId);
                    }
                }
                else
                {
                    query = "query { organization(login: \"" + username + "\") {pinnedItems(first: 6) {nodes {... on Repository {nameWithOwner}}}}}";
                    var repositoriesRequest = new GraphQLRequest { Query = query };

                    var graphQLResponse = await graphQLClient.SendQueryAsync<OrgResponseType>(repositoriesRequest);

                    foreach (var fullname in graphQLResponse.Data.Organization.PinnedItems.Nodes)
                    {
                        var repoId = await GetRepoIdFromRepoNameAndOwner(fullname.NameWithOwner);
                        repoIdList.Add(repoId);
                    }
                }

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

        public class UserResponseType
        {
            public UserNode User { get; set; }

            public class UserNode
            {
                public PinnedItemsNode PinnedItems { get; set; }

                public class PinnedItemsNode
                {
                    public List<NodeType> Nodes { get; set; }

                    public class NodeType
                    {
                        public string NameWithOwner { get; set; }
                    }
                }
            }
        }

        public class OrgResponseType
        {
            public OrganizationNode Organization { get; set; }

            public class OrganizationNode
            {
                public PinnedItemsNode PinnedItems { get; set; }

                public class PinnedItemsNode
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
