namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can represent a repository ruleset bypass actor.
    /// </summary>
    public class BypassActor
    {
        /// <summary>
        /// A GitHub App.
        /// </summary>
        public App App { get; set; }

        /// <summary>
        /// A team of users in an organization.
        /// </summary>
        public Team Team { get; set; }
    }
}