using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class RepositoryDetails
    {
        public int WatcherCount { get; set; }
        public int HeadRefsCount { get; set; }
        public int ReleaseCount { get; set; }

        public bool HasIssuesEnabled { get; set; }
        public bool HasProjectsEnabled { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsTemplate { get; set; }

        public string DefaultBranchName { get; set; }
        public string HomepageUrl { get; set; }

        public Release LatestReleaseOverview { get; set; } = new();

        public GraphQLModel.SubscriptionState? ViewerSubscription { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
