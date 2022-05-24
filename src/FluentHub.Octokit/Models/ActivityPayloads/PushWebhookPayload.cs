using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PushWebhookPayload
    {
        public string Head { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Ref { get; set; }
        public string BaseRef { get; set; }
        public bool Created { get; set; }
        public bool Deleted { get; set; }
        public bool Forced { get; set; }
        public string Compare { get; set; }
        public int Size { get; set; }
        public IReadOnlyList<OctokitOriginal.PushWebhookCommit> Commits { get; set; }
        public OctokitOriginal.PushWebhookCommit HeadCommit { get; set; }
    }
}
