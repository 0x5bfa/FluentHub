namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A version tag contains the mapping between a tag name and a version.
    /// </summary>
    public class PackageTag : QueryableValue<PackageTag>
    {
        internal PackageTag(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the PackageTag object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the tag name of the version.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Version that the tag is associated with.
        /// </summary>
        public PackageVersion Version => this.CreateProperty(x => x.Version, Octokit.GraphQL.Model.PackageVersion.Create);

        internal static PackageTag Create(Expression expression)
        {
            return new PackageTag(expression);
        }
    }
}