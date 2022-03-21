using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Models
{
    public class CommitOverviewItem
    {
        public string AuthorAvatarUrl { get; set; } // 100x100

        public string AuthorName { get; set; }

        public string CommitMessage { get; set; }

        public DateTimeOffset CommittedDate { get; set; }

        public string AbbreviatedOid { get; set; }

        public int TotalCount { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string ObjectType { get; set; }
    }
}
