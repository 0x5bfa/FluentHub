namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An individual package
    /// </summary>
    public class SecurityAdvisoryPackage
    {
        /// <summary>
        /// The ecosystem the package belongs to, e.g. RUBYGEMS, NPM
        /// </summary>
        public SecurityAdvisoryEcosystem Ecosystem { get; set; }

        /// <summary>
        /// The package name
        /// </summary>
        public string Name { get; set; }
    }
}