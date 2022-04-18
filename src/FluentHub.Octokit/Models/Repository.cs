using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Repository
    {
        public int ForkCount { get; set; }
        public int OpenIssueCount { get; set; }
        public int OpenPullCount { get; set; }
        public int StargazerCount { get; set; }

        public bool OwnerIsOrganization { get; set; }
        public bool IsFork { get; set; }
        public bool ViewerHasStarred { get; set; }

        public string Name { get; set; }
        public string OwnerAvatarUrl { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string PrimaryLangName { get; set; }
        public string PrimaryLangColor { get; set; }
        public string LicenseName { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
