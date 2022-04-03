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

        public bool IsClosed { get; set; }

        public List<Label> Labels { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
