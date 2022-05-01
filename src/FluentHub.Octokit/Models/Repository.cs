using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Repository
    {
        public int ForkCount { get; set; }
        public int OpenIssueCount { get; set; }
        public int OpenPullCount { get; set; }
        public int StargazerCount { get; set; }
        public int WatcherCount { get; set; }
        public int HeadRefsCount { get; set; }
        public int ReleaseCount { get; set; }
        public int TagCount { get; set; }

        public bool IsInOrganization { get; set; }
        public bool IsFork { get; set; }
        public bool ViewerHasStarred { get; set; }
        public bool HasIssuesEnabled { get; set; }
        public bool HasProjectsEnabled { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsTemplate { get; set; }
        public bool ForkingAllowed { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string LicenseName { get; set; }
        public string DefaultBranchName { get; set; }
        public string HomepageUrl { get; set; }

        public Release LatestRelease { get; set; }
        public RepositoryOwner Owner { get; set; }
        public Language PrimaryLanguage { get; set; }

        public GraphQLModel.SubscriptionState? ViewerSubscription { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
