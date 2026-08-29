namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The auto-trigger preferences that are available for check suites.
    /// </summary>
    public class CheckSuiteAutoTriggerPreference
    {
        /// <summary>
        /// The node ID of the application that owns the check suite.
        /// </summary>
        public ID AppId { get; set; }

        /// <summary>
        /// Set to `true` to enable automatic creation of CheckSuite events upon pushes to the repository.
        /// </summary>
        public bool Setting { get; set; }
    }
}