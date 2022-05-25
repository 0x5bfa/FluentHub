using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PullRequestEventPayload
    {

        public string Action { get; set; }

        public int Number { get; set; }
        public OctokitOriginal.PullRequest PullRequest { get; set; }
    }
}
