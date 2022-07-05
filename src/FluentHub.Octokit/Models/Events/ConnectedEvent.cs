using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.Events
{
    public class ConnectedEvent
    {
        public Actor Actor { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public string Id { get; set; }

        public bool IsCrossRepository { get; set; }

        public ReferencedSubject Source { get; set; }

        public ReferencedSubject Subject { get; set; }
    }
}
