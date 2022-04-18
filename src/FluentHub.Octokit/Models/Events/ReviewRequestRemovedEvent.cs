using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.Events
{
    public class ReviewRequestRemovedEvent
    {
        public string ActorLogin { get; set; }
        public string ActorAvatarUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
