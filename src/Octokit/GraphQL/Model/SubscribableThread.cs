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
    /// Entities that can be subscribed to for web and email notifications.
    /// </summary>
    [GraphQLIdentifier("SubscribableThread")]
    public interface ISubscribableThread : IQueryableValue<ISubscribableThread>, IQueryableInterface
    {
        /// <summary>
        /// The Node ID of the SubscribableThread object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Identifies the viewer's thread subscription form action.
        /// </summary>
        ThreadSubscriptionFormAction? ViewerThreadSubscriptionFormAction { get; }

        /// <summary>
        /// Identifies the viewer's thread subscription status.
        /// </summary>
        ThreadSubscriptionState? ViewerThreadSubscriptionStatus { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("SubscribableThread")]
    internal class StubISubscribableThread : QueryableValue<StubISubscribableThread>, ISubscribableThread
    {
        internal StubISubscribableThread(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        public ThreadSubscriptionFormAction? ViewerThreadSubscriptionFormAction { get; }

        public ThreadSubscriptionState? ViewerThreadSubscriptionStatus { get; }

        internal static StubISubscribableThread Create(Expression expression)
        {
            return new StubISubscribableThread(expression);
        }
    }
}