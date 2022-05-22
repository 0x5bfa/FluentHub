using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PushWebhookCommit
    {
        public string Id { get; set; }
        public string TreeId { get; set; }
        public bool Distinct { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Uri Url { get; set; }
        public OctokitOriginal.Committer Author { get; set; }
        public OctokitOriginal.Committer Committer { get; set; }
        public IReadOnlyList<string> Added { get; set; }
        public IReadOnlyList<string> Removed { get; set; }
        public IReadOnlyList<string> Modified { get; set; }
    }
}
