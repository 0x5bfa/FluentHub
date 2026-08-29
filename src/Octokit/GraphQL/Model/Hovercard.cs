namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Detail needed to display a hovercard for a user
    /// </summary>
    public class Hovercard : QueryableValue<Hovercard>
    {
        internal Hovercard(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Each of the contexts for this hovercard
        /// </summary>
        public IQueryableList<IHovercardContext> Contexts => this.CreateProperty(x => x.Contexts);

        internal static Hovercard Create(Expression expression)
        {
            return new Hovercard(expression);
        }
    }
}