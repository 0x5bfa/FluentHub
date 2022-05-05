using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Issue
    {
        public string Name { get; set; }
        public string OwnerLogin { get; set; }
        public string OwnerAvatarUrl { get; set; }
        public string Title { get; set; }

        public int Number { get; set; }
        public int CommentCount { get; set; }

        public bool Closed { get; set; }

        public List<Label> Labels { get; set; } = new();

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }
    }
}
