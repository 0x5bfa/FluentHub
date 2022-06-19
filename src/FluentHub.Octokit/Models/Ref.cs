namespace FluentHub.Octokit.Models
{
    public class Ref
    {
        //public PullRequestConnection AssociatedPullRequests { get; set; }

        //public BranchProtectionRule BranchProtectionRule { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Prefix { get; set; }

        //public RefUpdateRule RefUpdateRule { get; set; }

        public Repository Repository { get; set; }

        //public IGitObject Target { get; set; }
    }
}
