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
    /// Represents a contribution a user made on GitHub, such as opening an issue.
    /// </summary>
    [GraphQLIdentifier("Contribution")]
    public interface IContribution : IQueryableValue<IContribution>, IQueryableInterface
    {
        /// <summary>
        /// Whether this contribution is associated with a record you do not have access to. For
        /// example, your own 'first issue' contribution may have been made on a repository you can no
        /// longer access.
        /// </summary>
        bool IsRestricted { get; }

        /// <summary>
        /// When this contribution was made.
        /// </summary>
        DateTimeOffset OccurredAt { get; }

        /// <summary>
        /// The HTTP path for this contribution.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this contribution.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The user who made this contribution.
        /// </summary>
        User User { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Contribution")]
    internal class StubIContribution : QueryableValue<StubIContribution>, IContribution
    {
        internal StubIContribution(Expression expression) : base(expression)
        {
        }

        public bool IsRestricted { get; }

        public DateTimeOffset OccurredAt { get; }

        public string ResourcePath { get; }

        public string Url { get; }

        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static StubIContribution Create(Expression expression)
        {
            return new StubIContribution(expression);
        }
    }
}