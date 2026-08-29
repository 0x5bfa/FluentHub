namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A common weakness enumeration
    /// </summary>
    public class CWE : QueryableValue<CWE>
    {
        internal CWE(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The id of the CWE
        /// </summary>
        public string CweId { get; }

        /// <summary>
        /// A detailed description of this CWE
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The Node ID of the CWE object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of this CWE
        /// </summary>
        public string Name { get; }

        internal static CWE Create(Expression expression)
        {
            return new CWE(expression);
        }
    }
}