namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Used for return value of Repository.issueOrPullRequest.
    /// </summary>
    public class IssueOrPullRequest
    {
        
            /// <summary>
            /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
            /// </summary>
        public Issue Issue { get; set; }

            /// <summary>
            /// A repository pull request.
            /// </summary>
        public PullRequest PullRequest { get; set; }
    }
}