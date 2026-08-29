namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information for an uploaded package.
    /// </summary>
    public class Package : QueryableValue<Package>
    {
        internal Package(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the Package object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Find the latest version for the package.
        /// </summary>
        public PackageVersion LatestVersion => this.CreateProperty(x => x.LatestVersion, Octokit.GraphQL.Model.PackageVersion.Create);

        /// <summary>
        /// Identifies the name of the package.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Identifies the type of the package.
        /// </summary>
        public PackageType PackageType { get; }

        /// <summary>
        /// The repository this package belongs to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Statistics about package activity.
        /// </summary>
        public PackageStatistics Statistics => this.CreateProperty(x => x.Statistics, Octokit.GraphQL.Model.PackageStatistics.Create);

        /// <summary>
        /// Find package version by version string.
        /// </summary>
        /// <param name="version">The package version.</param>
        public PackageVersion Version(Arg<string> version) => this.CreateMethodCall(x => x.Version(version), Octokit.GraphQL.Model.PackageVersion.Create);

        /// <summary>
        /// list of versions for this package
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering of the returned packages.</param>
        public PackageVersionConnection Versions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<PackageVersionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Versions(first, after, last, before, orderBy), Octokit.GraphQL.Model.PackageVersionConnection.Create);

        internal static Package Create(Expression expression)
        {
            return new Package(expression);
        }
    }
}