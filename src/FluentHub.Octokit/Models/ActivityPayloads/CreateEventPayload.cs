using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class CreateEventPayload
    {
        public string Ref { get; set; }
        public OctokitOriginal.StringEnum<OctokitOriginal.RefType> RefType { get; set; }
        public string MasterBranch { get; set; }
        public string Description { get; set; }
    }
}
