namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A version tag contains the mapping between a tag name and a version.
    /// </summary>
    public class PackageTag
    {
        public ID Id { get; set; }

        /// <summary>
        /// Identifies the tag name of the version.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version that the tag is associated with.
        /// </summary>
        public PackageVersion Version { get; set; }
    }
}