using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Commit
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Refs { get; set; }

        public string AuthorAvatarUrl { get; set; }
        public string AuthorName { get; set; }
        public string CommitMessage { get; set; }
        public string CommitMessageHeadline { get; set; }
        public string AbbreviatedOid { get; set; }
        public string Oid { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ObjectType { get; set; }

        public int TotalCount { get; set; }
        public int PullRequestNumber { get; set; }

        public DateTimeOffset CommittedAt { get; set; }
        public string CommittedAtHumanized { get; set; }
    }
}
