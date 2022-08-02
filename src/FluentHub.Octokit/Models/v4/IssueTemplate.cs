namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository issue template.
    /// </summary>
    public class IssueTemplate
    {
        /// <summary>
        /// The template purpose.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// The suggested issue body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The template name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The suggested issue title.
        /// </summary>
        public string Title { get; set; }
    }
}