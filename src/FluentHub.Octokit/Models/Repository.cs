using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Repository
    {
        public string Name { get; set; }

        public string OwnerAvatarUrl { get; set; }

        public string Owner { get; set; }

        public bool OwnerIsUser { get; set; }

        public string Description { get; set; }

        public string PrimaryLangName { get; set; }

        public string PrimaryLangColor { get; set; }

        public string LicenseName { get; set; }

        public int ForkCount { get; set; }

        public int IssueCount { get; set; }

        public int PullCount { get; set; }

        public int StargazerCount { get; set; }

        public int WatcherCount { get; set; }

        public string DefaultBranchName { get; set; }

        public string CloneUrl { get; set; }

        public string SshUrl { get; set; }

        public string GitUrl { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        // Used to change repository's branch
        public string CurrentBranch { get; set; }
    }
}
