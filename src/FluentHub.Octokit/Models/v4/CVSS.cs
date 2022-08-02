namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The Common Vulnerability Scoring System
    /// </summary>
    public class CVSS
    {
        /// <summary>
        /// The CVSS score associated with this advisory
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The CVSS vector string associated with this advisory
        /// </summary>
        public string VectorString { get; set; }
    }
}