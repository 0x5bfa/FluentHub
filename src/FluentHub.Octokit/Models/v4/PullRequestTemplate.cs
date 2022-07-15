namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository pull request template.
    /// </summary>
    public class PullRequestTemplate
    {
        /// <summary>
        /// The body of the template
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The filename of the template
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// The repository the template belongs to
        /// </summary>
        public Repository Repository { get; set; }
    }
}