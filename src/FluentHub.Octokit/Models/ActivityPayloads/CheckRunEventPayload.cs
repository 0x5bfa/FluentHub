using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class CheckRunEventPayload
    {
        public string Action { get; set; }

        public OctokitOriginal.CheckRun CheckRun { get; set; }
        public OctokitOriginal.CheckRunRequestedAction RequestedAction { get; set; }
    }
}
