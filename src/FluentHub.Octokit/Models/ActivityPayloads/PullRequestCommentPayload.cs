using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class PullRequestCommentPayload
    {
        public string Action { get; set; }

        public OctokitOriginal.PullRequest PullRequest { get; set; }
        public OctokitOriginal.PullRequestReviewComment Comment { get; set; }
    }
}
