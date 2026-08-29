namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a object that contains package version activity statistics such as downloads.
    /// </summary>
    public class PackageVersionStatistics : QueryableValue<PackageVersionStatistics>
    {
        internal PackageVersionStatistics(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Number of times the package was downloaded since it was created.
        /// </summary>
        public int DownloadsTotalCount { get; }

        internal static PackageVersionStatistics Create(Expression expression)
        {
            return new PackageVersionStatistics(expression);
        }
    }
}