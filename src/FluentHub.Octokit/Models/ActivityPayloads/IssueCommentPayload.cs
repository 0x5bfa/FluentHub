using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class IssueCommentPayload
    {
        // should always be "created" according to github api docs
        public string Action { get; set; }

        public OctokitOriginal.Issue Issue { get; set; }
        public OctokitOriginal.IssueComment Comment { get; set; }
    }
}
