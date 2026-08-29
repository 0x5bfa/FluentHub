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
    [GraphQLIdentifier("Subscribable")]
    public interface ISubscribable : IQueryableValue<ISubscribable>, IQueryableInterface
    {
        /// <summary>
        /// The Node ID of the Subscribable object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        SubscriptionState? ViewerSubscription { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Subscribable")]
    internal class StubISubscribable : QueryableValue<StubISubscribable>, ISubscribable
    {
        internal StubISubscribable(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        public bool ViewerCanSubscribe { get; }

        public SubscriptionState? ViewerSubscription { get; }

        internal static StubISubscribable Create(Expression expression)
        {
            return new StubISubscribable(expression);
        }
    }
}