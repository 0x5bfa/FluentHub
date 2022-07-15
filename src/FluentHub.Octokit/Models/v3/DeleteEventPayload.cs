using System.Collections.Generic;
using System.Diagnostics;

namespace FluentHub.Octokit.Models.v3
{
    public class DeleteEventPayload : ActivityPayload
    {
        public string Ref { get; set; }

        //public StringEnum<RefType> RefType { get; set; }
    }
}
