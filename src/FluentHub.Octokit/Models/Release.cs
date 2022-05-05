using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Release
    {
        public string AuthorLogin { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string DescriptionHTML { get; set; }
        public string Name { get; set; }

        public bool IsDraft { get; set; }
        public bool IsLatest { get; set; }
        public bool IsPrerelease { get; set; }

        public DateTimeOffset? PublishedAt { get; set; }
        public string PublishedAtHumanized { get; set; }
    }
}
