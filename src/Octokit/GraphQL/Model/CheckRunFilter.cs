namespace Octokit.GraphQL.Model
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
        /// Filters the check runs by this status. Superceded by statuses.
        /// </summary>
        public CheckStatusState? Status { get; set; }

        /// <summary>
        /// Filters the check runs by this status. Overrides status.
        /// </summary>
        public IEnumerable<CheckStatusState> Statuses { get; set; }

        /// <summary>
        /// Filters the check runs by these conclusions.
        /// </summary>
        public IEnumerable<CheckConclusionState> Conclusions { get; set; }
    }
}