namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Describes a License's conditions, permissions, and limitations
    /// </summary>
    public class LicenseRule : QueryableValue<LicenseRule>
    {
        internal LicenseRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A description of the rule
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The machine-readable rule key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The human-readable rule label
        /// </summary>
        public string Label { get; }

        internal static LicenseRule Create(Expression expression)
        {
            return new LicenseRule(expression);
        }
    }
}