using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PushEventPayload
    {
        public string Head { get; set; }
        public string Ref { get; set; }
        public int Size { get; set; }
        public IReadOnlyList<OctokitOriginal.Commit> Commits { get; set; }
    }
}
