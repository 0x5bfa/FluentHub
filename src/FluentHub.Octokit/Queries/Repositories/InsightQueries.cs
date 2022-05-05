using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OctokitOriginal = global::Octokit;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class InsightQueries
    {
        public async Task GetContributors(string owner, string name)
        {
            // just testing
            var codefrequency = await App.Client.Repository.Statistics.GetCodeFrequency(owner, name);
            var commitactivity = await App.Client.Repository.Statistics.GetCommitActivity(owner, name);
            var contributors = await App.Client.Repository.Statistics.GetContributors(owner, name);
            var participation = await App.Client.Repository.Statistics.GetParticipation(owner, name);
            var allreferrers  = await App.Client.Repository.Traffic.GetAllReferrers(owner, name);
            var clones = await App.Client.Repository.Traffic.GetClones(owner, name, new OctokitOriginal.RepositoryTrafficRequest());
            var views = await App.Client.Repository.Traffic.GetViews(owner, name, new OctokitOriginal.RepositoryTrafficRequest());
            var allpaths = await App.Client.Repository.Traffic.GetAllPaths(owner, name);
        }
    }
}
