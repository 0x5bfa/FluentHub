using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models.ActivityPayloads
{
    public class StatusEventPayload
    {
        public string Name { get; set; }
        public string Sha { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public OctokitOriginal.StringEnum<OctokitOriginal.CommitState> State { get; set; }
        public string TargetUrl { get; set; }
        public string Description { get; set; }
        public string Context { get; set; }
        public long Id { get; set; }
        public OctokitOriginal.GitHubCommit Commit { get; set; }
        public OctokitOriginal.Organization Organization { get; set; }
        public IReadOnlyList<OctokitOriginal.Branch> Branches { get; set; }
    }
}
