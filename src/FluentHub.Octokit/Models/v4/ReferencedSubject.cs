namespace FluentHub.Octokit.v4.Model
{
    using System;

    /// <summary>
    /// Any referencable object
    /// </summary>
    public class ReferencedSubject
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