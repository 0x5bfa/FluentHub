using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.Events
{
    public class LabeledEvent
    {
        public string ActorLogin { get; set; }
        public string ActorAvatarUrl { get; set; }

        public Label LabeledLabel { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }
    }
}
