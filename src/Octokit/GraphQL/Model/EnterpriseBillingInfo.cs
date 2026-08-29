namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Enterprise billing information visible to enterprise billing managers and owners.
    /// </summary>
    public class EnterpriseBillingInfo : QueryableValue<EnterpriseBillingInfo>
    {
        internal EnterpriseBillingInfo(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of licenseable users/emails across the enterprise.
        /// </summary>
        public int AllLicensableUsersCount { get; }

        /// <summary>
        /// The number of data packs used by all organizations owned by the enterprise.
        /// </summary>
        public int AssetPacks { get; }

        /// <summary>
        /// The bandwidth quota in GB for all organizations owned by the enterprise.
        /// </summary>
        public double BandwidthQuota { get; }

        /// <summary>
        /// The bandwidth usage in GB for all organizations owned by the enterprise.
        /// </summary>
        public double BandwidthUsage { get; }

        /// <summary>
        /// The bandwidth usage as a percentage of the bandwidth quota.
        /// </summary>
        public int BandwidthUsagePercentage { get; }

        /// <summary>
        /// The storage quota in GB for all organizations owned by the enterprise.
        /// </summary>
        public double StorageQuota { get; }

        /// <summary>
        /// The storage usage in GB for all organizations owned by the enterprise.
        /// </summary>
        public double StorageUsage { get; }

        /// <summary>
        /// The storage usage as a percentage of the storage quota.
        /// </summary>
        public int StorageUsagePercentage { get; }

        /// <summary>
        /// The number of available licenses across all owned organizations based on the unique number of billable users.
        /// </summary>
        public int TotalAvailableLicenses { get; }

        /// <summary>
        /// The total number of licenses allocated.
        /// </summary>
        public int TotalLicenses { get; }

        internal static EnterpriseBillingInfo Create(Expression expression)
        {
            return new EnterpriseBillingInfo(expression);
        }
    }
}