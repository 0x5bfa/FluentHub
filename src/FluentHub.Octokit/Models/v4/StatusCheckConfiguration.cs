namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Required status check
    /// </summary>
    public class StatusCheckConfiguration
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