using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class CommitCommentPayload
    {
        public OctokitOriginal.CommitComment Comment { get; set; }
    }
}
