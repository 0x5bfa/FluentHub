namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The object which triggered a `ClosedEvent`.
    /// </summary>
    public class Closer
    {
        
        /// <summary>
        /// Represents a Git commit.
        /// </summary>
            public Commit Commit { get; set; }

        /// <summary>
        /// A repository pull request.
        /// </summary>
            public PullRequest PullRequest { get; set; }
    }
}