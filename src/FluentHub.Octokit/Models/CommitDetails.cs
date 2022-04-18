using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class CommitDetails
    {
        public string CommitMessageHeadline { get; set; }
        public string CommitMessage { get; set; }

        public string ParentOid { get; set; }
        public string AbbreviatedParentOid { get; set; }
        public string Oid { get; set; }
        public string AbbreviatedOid { get; set; }

        public int TotalChangedFileCount { get; set; }
        public int TotalAdditions { get; set; }
        public int TotalDeletions { get; set; }

        public List<ChangedFile> ChangedFiles { get; set; } = new();
    }
}
