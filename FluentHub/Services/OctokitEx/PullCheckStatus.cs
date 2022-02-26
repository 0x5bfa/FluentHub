using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.OctokitEx
{
    public class PullCheckStatus
    {
        public async Task<CheckStatuses> GetLatestCheckStatus(string owner, string name, int number)
        {
            try
            {
                string token = App.Settings.Get("AccessToken", "");

                var graphQLClient = new GraphQLHttpClient($"{App.GraphQlApiEndPoint}", new NewtonsoftJsonSerializer());

                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

                string query = null;

                List<long> repoIdList = new List<long>();

                query = "{repository (name: \"" + name + "\",owner: \"" + owner + "\") {pullRequest(number: " + number + ") {commits(last: 1){ nodes{ commit{ statusCheckRollup { state }}}}}}}";

                var repositoriesRequest = new GraphQLRequest { Query = query };

                var graphQLResponse = await graphQLClient.SendQueryAsync<LatestReviewStatusResponseType>(repositoriesRequest);

                switch (graphQLResponse.Data.Repository.PullRequest.Commits.Nodes[0].Commit.StatusCheckRollup.State)
                {
                    case "EXPECTED":
                        return CheckStatuses.EXPECTED;
                    case "ERROR":
                        return CheckStatuses.ERROR;
                    case "FAILURE":
                        return CheckStatuses.FAILURE;
                    case "PENDING":
                        return CheckStatuses.PENDING;
                    case "SUCCESS":
                        return CheckStatuses.SUCCESS;
                    default:
                        return CheckStatuses.UNKNOWN;
                }
            }
            catch
            {
                return CheckStatuses.UNKNOWN;
            }
        }

        public enum CheckStatuses
        {
            UNKNOWN,
            EXPECTED,
            ERROR,
            FAILURE,
            PENDING,
            SUCCESS
        }

        public class LatestReviewStatusResponseType
        {
            public RepositoryNode Repository { get; set; }

            public class RepositoryNode
            {
                public PullRequestNode PullRequest { get; set; }

                public class PullRequestNode
                {
                    public CommitsNode Commits { get; set; }

                    public class CommitsNode
                    {
                        public List<NodeType> Nodes { get; set; }

                        public class NodeType
                        {
                            public CommitNode Commit { get; set; }

                            public class CommitNode
                            {
                                public StatusCheckRollupNode StatusCheckRollup { get; set; }

                                public class StatusCheckRollupNode
                                {
                                    public string State { get; set; }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
