namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A hovercard context with a message describing how the viewer is related.
    /// </summary>
    public class ViewerHovercardContext : QueryableValue<ViewerHovercardContext>
    {
        internal ViewerHovercardContext(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; }

        /// <summary>
        /// Identifies the user who is related to this context.
        /// </summary>
        public User Viewer => this.CreateProperty(x => x.Viewer, Octokit.GraphQL.Model.User.Create);

        internal static ViewerHovercardContext Create(Expression expression)
        {
            return new ViewerHovercardContext(expression);
        }
    }
}