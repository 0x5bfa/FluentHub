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
    /// Represents an object which can take actions on GitHub. Typically a User or Bot.
    /// </summary>
    [GraphQLIdentifier("Actor")]
    public interface IActor : IQueryableValue<IActor>, IQueryableInterface
    {
        /// <summary>
        /// A URL pointing to the actor's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarUrl(Arg<int>? size = null);

        /// <summary>
        /// The username of the actor.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// The HTTP path for this actor.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this actor.
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Actor")]
    internal class StubIActor : QueryableValue<StubIActor>, IActor
    {
        internal StubIActor(Expression expression) : base(expression)
        {
        }

        public string AvatarUrl(Arg<int>? size = null) => default;

        public string Login { get; }

        public string ResourcePath { get; }

        public string Url { get; }

        internal static StubIActor Create(Expression expression)
        {
            return new StubIActor(expression);
        }
    }
}