namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Required status check
    /// </summary>
    public class StatusCheckConfigurationInput
    {
        /// <summary>
        /// The status check context name that must be present on the commit.
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// The optional integration ID that this status check must originate from.
        /// </summary>
        public int? IntegrationId { get; set; }
    }
}