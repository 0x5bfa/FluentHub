namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A file in a package version.
    /// </summary>
    public class PackageFile : QueryableValue<PackageFile>
    {
        internal PackageFile(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the PackageFile object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// MD5 hash of the file.
        /// </summary>
        public string Md5 { get; }

        /// <summary>
        /// Name of the file.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The package version this file belongs to.
        /// </summary>
        public PackageVersion PackageVersion => this.CreateProperty(x => x.PackageVersion, Octokit.GraphQL.Model.PackageVersion.Create);

        /// <summary>
        /// SHA1 hash of the file.
        /// </summary>
        public string Sha1 { get; }

        /// <summary>
        /// SHA256 hash of the file.
        /// </summary>
        public string Sha256 { get; }

        /// <summary>
        /// Size of the file in bytes.
        /// </summary>
        public int? Size { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// URL to download the asset.
        /// </summary>
        public string Url { get; }

        internal static PackageFile Create(Expression expression)
        {
            return new PackageFile(expression);
        }
    }
}