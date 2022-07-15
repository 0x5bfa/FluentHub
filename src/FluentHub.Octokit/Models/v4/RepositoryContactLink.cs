namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository contact link.
    /// </summary>
    public class RepositoryContactLink
    {
        /// <summary>
        /// The contact link purpose.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// The contact link name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The contact link URL.
        /// </summary>
        public string Url { get; set; }
    }
}