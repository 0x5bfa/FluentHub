namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Enterprise billing information visible to enterprise billing managers and owners.
    /// </summary>
    public class EnterpriseBillingInfo
    {
        /// <summary>
        /// The number of licenseable users/emails across the enterprise.
        /// </summary>
        public int AllLicensableUsersCount { get; set; }

        /// <summary>
        /// The number of data packs used by all organizations owned by the enterprise.
        /// </summary>
        public int AssetPacks { get; set; }

        /// <summary>
        /// The bandwidth quota in GB for all organizations owned by the enterprise.
        /// </summary>
        public double BandwidthQuota { get; set; }

        /// <summary>
        /// The bandwidth usage in GB for all organizations owned by the enterprise.
        /// </summary>
        public double BandwidthUsage { get; set; }

        /// <summary>
        /// The bandwidth usage as a percentage of the bandwidth quota.
        /// </summary>
        public int BandwidthUsagePercentage { get; set; }

        /// <summary>
        /// The storage quota in GB for all organizations owned by the enterprise.
        /// </summary>
        public double StorageQuota { get; set; }

        /// <summary>
        /// The storage usage in GB for all organizations owned by the enterprise.
        /// </summary>
        public double StorageUsage { get; set; }

        /// <summary>
        /// The storage usage as a percentage of the storage quota.
        /// </summary>
        public int StorageUsagePercentage { get; set; }

        /// <summary>
        /// The number of available licenses across all owned organizations based on the unique number of billable users.
        /// </summary>
        public int TotalAvailableLicenses { get; set; }

        /// <summary>
        /// The total number of licenses allocated.
        /// </summary>
        public int TotalLicenses { get; set; }
    }
}