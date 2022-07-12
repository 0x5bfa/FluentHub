namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can be inside a Milestone.
    /// </summary>
    public class MilestoneItem
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