namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A file in a package version.
    /// </summary>
    public class PackageFile
    {
        public ID Id { get; set; }

        /// <summary>
        /// MD5 hash of the file.
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// Name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The package version this file belongs to.
        /// </summary>
        public PackageVersion PackageVersion { get; set; }

        /// <summary>
        /// SHA1 hash of the file.
        /// </summary>
        public string Sha1 { get; set; }

        /// <summary>
        /// SHA256 hash of the file.
        /// </summary>
        public string Sha256 { get; set; }

        /// <summary>
        /// Size of the file in bytes.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// URL to download the asset.
        /// </summary>
        public string Url { get; set; }
    }
}