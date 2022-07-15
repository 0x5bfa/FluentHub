namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An individual package version
    /// </summary>
    public class SecurityAdvisoryPackageVersion
    {
        /// <summary>
        /// The package name or version
        /// </summary>
        public string Identifier { get; set; }
    }
}