using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.Events
{
    public class HeadRefForcePushedEvent
    {
        public Actor Actor { get; set; }

        public Commit AfterCommit { get; set; }

        public Commit BeforeCommit { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public Ref Ref { get; set; }
    }
}
