namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The filters that are available when fetching check runs.
    /// </summary>
    public class CheckRunFilter
    {
        /// <summary>
        /// Filters the check runs by this type.
        /// </summary>
        public CheckRunType? CheckType { get; set; }

        /// <summary>
        /// Filters the check runs created by this application ID.
        /// </summary>
        public int? AppId { get; set; }

        /// <summary>
        /// Filters the check runs by this name.
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// Filters the check runs by this status.
        /// </summary>
        public CheckStatusState? Status { get; set; }
    }
}