using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class DeleteEventPayload
    {
        public string Ref { get; set; }
        public OctokitOriginal.StringEnum<OctokitOriginal.RefType> RefType { get; set; }
    }
}
