using System.Diagnostics;

namespace FluentHub.Octokit.Models.v3
{
    public class IssueEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }

        public Issue Issue { get; protected set; }
    }
}
