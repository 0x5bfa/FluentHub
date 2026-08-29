namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information about a specific package version.
    /// </summary>
    public class PackageVersion : QueryableValue<PackageVersion>
    {
        internal PackageVersion(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// List of files associated with this package version
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering of the returned package files.</param>
        public PackageFileConnection Files(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<PackageFileOrder>? orderBy = null) => this.CreateMethodCall(x => x.Files(first, after, last, before, orderBy), Octokit.GraphQL.Model.PackageFileConnection.Create);

        /// <summary>
        /// The Node ID of the PackageVersion object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The package associated with this version.
        /// </summary>
        public Package Package => this.CreateProperty(x => x.Package, Octokit.GraphQL.Model.Package.Create);

        /// <summary>
        /// The platform this version was built for.
        /// </summary>
        public string Platform { get; }

        /// <summary>
        /// Whether or not this version is a pre-release.
        /// </summary>
        public bool PreRelease { get; }

        /// <summary>
        /// The README of this package version.
        /// </summary>
        public string Readme { get; }

        /// <summary>
        /// The release associated with this package version.
        /// </summary>
        public Release Release => this.CreateProperty(x => x.Release, Octokit.GraphQL.Model.Release.Create);

        /// <summary>
        /// Statistics about package activity.
        /// </summary>
        public PackageVersionStatistics Statistics => this.CreateProperty(x => x.Statistics, Octokit.GraphQL.Model.PackageVersionStatistics.Create);

        /// <summary>
        /// The package version summary.
        /// </summary>
        public string Summary { get; }

        /// <summary>
        /// The version string.
        /// </summary>
        public string Version { get; }

        internal static PackageVersion Create(Expression expression)
        {
            return new PackageVersion(expression);
        }
    }
}