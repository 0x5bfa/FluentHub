namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A branch linked to an issue.
    /// </summary>
    public class LinkedBranch : QueryableValue<LinkedBranch>
    {
        internal LinkedBranch(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the LinkedBranch object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The branch's ref.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Model.Ref.Create);

        internal static LinkedBranch Create(Expression expression)
        {
            return new LinkedBranch(expression);
        }
    }
}