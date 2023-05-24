namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A GitHub Enterprise Importer (GEI) migration source.
    /// </summary>
    public class MigrationSource
    {
        public ID Id { get; set; }

        /// <summary>
        /// The migration source name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The migration source type.
        /// </summary>
        public MigrationSourceType Type { get; set; }

        /// <summary>
        /// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
        /// </summary>
        public string Url { get; set; }
    }
}