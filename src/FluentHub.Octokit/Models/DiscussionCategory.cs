using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class DiscussionCategory
    {
        public string Description { get; set; }
        public string Emoji { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
