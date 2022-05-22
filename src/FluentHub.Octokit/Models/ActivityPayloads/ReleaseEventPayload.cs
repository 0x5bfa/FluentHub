using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class ReleaseEventPayload
    {
        public string Action { get; set; }

        public OctokitOriginal.Release Release { get; set; }
    }
}
