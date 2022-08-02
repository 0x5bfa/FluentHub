namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A GitHub Security Advisory Identifier
    /// </summary>
    public class SecurityAdvisoryIdentifier
    {
        /// <summary>
        /// The identifier type, e.g. GHSA, CVE
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The identifier
        /// </summary>
        public string Value { get; set; }
    }
}