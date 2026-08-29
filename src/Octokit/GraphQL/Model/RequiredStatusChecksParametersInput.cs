namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Choose which status checks must pass before the ref is updated. When enabled, commits must first be pushed to another ref where the checks pass.
    /// </summary>
    public class RequiredStatusChecksParametersInput
    {
        /// <summary>
        /// Status checks that are required.
        /// </summary>
        public IEnumerable<StatusCheckConfigurationInput> RequiredStatusChecks { get; set; }

        /// <summary>
        /// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
        /// </summary>
        public bool StrictRequiredStatusChecksPolicy { get; set; }
    }
}