namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An Octoshift migration source.
    /// </summary>
    public class MigrationSource
    {
        public ID Id { get; set; }

        /// <summary>
        /// The Octoshift migration source name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Octoshift migration source type.
        /// </summary>
        public MigrationSourceType Type { get; set; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        public string Url { get; set; }
    }
}