using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string OwnerAvatarUrl { get; set; }
        public string Owner { get; set; }

        public string Description { get; set; }
        public string PrimaryLangName { get; set; }
        public string PrimaryLangColor { get; set; }
        public string LicenseName { get; set; }

        public int ForkCount { get; set; }
        public int IssueCount { get; set; }
        public int OpenIssueCount { get; set; }
        public int PullCount { get; set; }
        public int OpenPullCount { get; set; }
        public int StargazerCount { get; set; }
        public int WatcherCount { get; set; }
        public int HeadRefsCount { get; set; }
        public int ReleaseCount { get; set; }

        public string DefaultBranchName { get; set; }
        public string HomepageUrl { get; set; }

        public bool ViewerHasStarred { get; set; }
        public bool HasIssuesEnabled { get; set; }
        public bool HasProjectsEnabled { get; set; }
        public bool IsArchived { get; set; }
        public bool IsFork { get; set; }
        public bool IsInOrganization { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsTemplate { get; set; }

        public GraphQLModel.SubscriptionState? ViewerSubscription { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
