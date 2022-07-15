namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a object that contains package version activity statistics such as downloads.
    /// </summary>
    public class PackageVersionStatistics
    {
        /// <summary>
        /// Number of times the package was downloaded since it was created.
        /// </summary>
        public int DownloadsTotalCount { get; set; }
    }
}