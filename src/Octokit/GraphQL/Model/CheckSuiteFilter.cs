namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The filters that are available when fetching check suites.
    /// </summary>
    public class CheckSuiteFilter
    {
        /// <summary>
        /// Filters the check suites created by this application ID.
        /// </summary>
        public int? AppId { get; set; }

        /// <summary>
        /// Filters the check suites by this name.
        /// </summary>
        public string CheckName { get; set; }
    }
}