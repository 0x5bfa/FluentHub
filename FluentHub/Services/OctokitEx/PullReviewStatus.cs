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
    public class PullReviewStatus
    {
        public async Task<Octokit.PullRequestReview> GetLatestReviewStatus(long repositoryId, int number)
        {
            var reviews = await App.Client.PullRequest.Review.GetAll(repositoryId, number);

            return (reviews[0] ?? null);
        }
    }
}
