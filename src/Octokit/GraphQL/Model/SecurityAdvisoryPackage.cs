namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An individual package
    /// </summary>
    public class SecurityAdvisoryPackage : QueryableValue<SecurityAdvisoryPackage>
    {
        internal SecurityAdvisoryPackage(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The ecosystem the package belongs to, e.g. RUBYGEMS, NPM
        /// </summary>
        public SecurityAdvisoryEcosystem Ecosystem { get; }

        /// <summary>
        /// The package name
        /// </summary>
        public string Name { get; }

        internal static SecurityAdvisoryPackage Create(Expression expression)
        {
            return new SecurityAdvisoryPackage(expression);
        }
    }
}