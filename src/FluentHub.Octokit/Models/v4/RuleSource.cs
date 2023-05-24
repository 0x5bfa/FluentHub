namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types which can have `RepositoryRule` objects.
    /// </summary>
    public class RuleSource
    {
        /// <summary>
        /// An account on GitHub, with one or more owners, that has repositories, members and teams.
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// A repository contains the content for a project.
        /// </summary>
        public Repository Repository { get; set; }
    }
}