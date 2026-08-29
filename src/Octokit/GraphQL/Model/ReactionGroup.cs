namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A group of emoji reactions to a particular piece of content.
    /// </summary>
    public class ReactionGroup : QueryableValue<ReactionGroup>
    {
        internal ReactionGroup(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the emoji reaction.
        /// </summary>
        public ReactionContent Content { get; }

        /// <summary>
        /// Identifies when the reaction was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// Reactors to the reaction subject with the emotion represented by this reaction group.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReactorConnection Reactors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Reactors(first, after, last, before), Octokit.GraphQL.Model.ReactorConnection.Create);

        /// <summary>
        /// The subject that was reacted to.
        /// </summary>
        public IReactable Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.Internal.StubIReactable.Create);

        /// <summary>
        /// Users who have reacted to the reaction subject with the emotion represented by this reaction group
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReactingUserConnection Users(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Users(first, after, last, before), Octokit.GraphQL.Model.ReactingUserConnection.Create);

        /// <summary>
        /// Whether or not the authenticated user has left a reaction on the subject.
        /// </summary>
        public bool ViewerHasReacted { get; }

        internal static ReactionGroup Create(Expression expression)
        {
            return new ReactionGroup(expression);
        }
    }
}