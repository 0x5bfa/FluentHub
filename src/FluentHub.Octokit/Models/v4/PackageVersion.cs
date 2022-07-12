namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Information about a specific package version.
    /// </summary>
    public class PackageVersion
    {
        /// <summary>
        /// List of files associated with this package version
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering of the returned package files.</param>
        public PackageFileConnection Files { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The package associated with this version.
        /// </summary>
        public Package Package { get; set; }

        /// <summary>
        /// The platform this version was built for.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Whether or not this version is a pre-release.
        /// </summary>
        public bool PreRelease { get; set; }

        /// <summary>
        /// The README of this package version.
        /// </summary>
        public string Readme { get; set; }

        /// <summary>
        /// The release associated with this package version.
        /// </summary>
        public Release Release { get; set; }

        /// <summary>
        /// Statistics about package activity.
        /// </summary>
        public PackageVersionStatistics Statistics { get; set; }

        /// <summary>
        /// The package version summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The version string.
        /// </summary>
        public string Version { get; set; }
    }
}