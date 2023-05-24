namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parameters to be used for the required_status_checks rule
    /// </summary>
    public class RequiredStatusChecksParametersInput
    {
        /// <summary>
        /// Status checks that are required.
        /// </summary>
        public List<StatusCheckConfigurationInput> RequiredStatusChecks { get; set; }

        /// <summary>
        /// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
        /// </summary>
        public bool StrictRequiredStatusChecksPolicy { get; set; }
    }
}