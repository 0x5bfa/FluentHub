﻿namespace FluentHub.Octokit.Queries.Repositories
{
    public class InsightQueries
    {
        public async Task GetContributors(string owner, string name)
        {
            // just testing (one of those api requests requires push access rights)
            //var codefrequency = await App.Client.Repository.Statistics.GetCodeFrequency(owner, name);
            //var commitactivity = await App.Client.Repository.Statistics.GetCommitActivity(owner, name);
            //var contributors = await App.Client.Repository.Statistics.GetContributors(owner, name);
            //var participation = await App.Client.Repository.Statistics.GetParticipation(owner, name);
            //var allreferrers  = await App.Client.Repository.Traffic.GetAllReferrers(owner, name);
            //var clones = await App.Client.Repository.Traffic.GetClones(owner, name, new OctokitV3.RepositoryTrafficRequest());
            //var views = await App.Client.Repository.Traffic.GetViews(owner, name, new OctokitV3.RepositoryTrafficRequest());
            //var allpaths = await App.Client.Repository.Traffic.GetAllPaths(owner, name);
        }
    }
}
