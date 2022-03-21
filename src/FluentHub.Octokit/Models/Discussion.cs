using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class Discussion
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public int Number { get; set; }

        public string CategoryName { get; set; }

        public string CategoryEmoji { get; set; }

        public int UpvoteCount { get; set; }

        public int TotalCommentCount { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
