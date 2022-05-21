using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class StatusCheckRollup
    {
        public OctokitGraphQLModel.StatusState Status { get; set; }

        public List<CheckRun> Contexts { get; set; }
    }
}
