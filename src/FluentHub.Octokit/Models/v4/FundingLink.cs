namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A funding platform link for a repository.
    /// </summary>
    public class FundingLink
    {
        /// <summary>
        /// The funding platform this link is for.
        /// </summary>
        public FundingPlatform Platform { get; set; }

        /// <summary>
        /// The configured URL for this funding link.
        /// </summary>
        public string Url { get; set; }
    }
}