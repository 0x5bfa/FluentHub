using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class CheckSuiteEventPayload
    {
        public string Action { get; set; }

        public OctokitOriginal.CheckSuite CheckSuite { get; set; }
    }
}
