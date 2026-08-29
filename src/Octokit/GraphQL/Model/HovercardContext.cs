namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An individual line of a hovercard
    /// </summary>
    [GraphQLIdentifier("HovercardContext")]
    public interface IHovercardContext : IQueryableValue<IHovercardContext>, IQueryableInterface
    {
        /// <summary>
        /// A string describing this context
        /// </summary>
        string Message { get; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        string Octicon { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("HovercardContext")]
    internal class StubIHovercardContext : QueryableValue<StubIHovercardContext>, IHovercardContext
    {
        internal StubIHovercardContext(Expression expression) : base(expression)
        {
        }

        public string Message { get; }

        public string Octicon { get; }

        internal static StubIHovercardContext Create(Expression expression)
        {
            return new StubIHovercardContext(expression);
        }
    }
}