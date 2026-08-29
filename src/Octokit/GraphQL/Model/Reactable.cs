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
    /// Represents a subject that can be reacted on.
    /// </summary>
    [GraphQLIdentifier("Reactable")]
    public interface IReactable : IQueryableValue<IReactable>, IQueryableInterface
    {
        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Reactable object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        IQueryableList<ReactionGroup> ReactionGroups { get; }

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        ReactionConnection Reactions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReactionContent>? content = null, Arg<ReactionOrder>? orderBy = null);

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        bool ViewerCanReact { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Reactable")]
    internal class StubIReactable : QueryableValue<StubIReactable>, IReactable
    {
        internal StubIReactable(Expression expression) : base(expression)
        {
        }

        public long? DatabaseId { get; }

        public ID Id { get; }

        public IQueryableList<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        public ReactionConnection Reactions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReactionContent>? content = null, Arg<ReactionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octokit.GraphQL.Model.ReactionConnection.Create);

        public bool ViewerCanReact { get; }

        internal static StubIReactable Create(Expression expression)
        {
            return new StubIReactable(expression);
        }
    }
}