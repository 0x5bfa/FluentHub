namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A group of emoji reactions to a particular piece of content.
    /// </summary>
    public class ReactionGroup
    {
        /// <summary>
        /// Identifies the emoji reaction.
        /// </summary>
        public ReactionContent Content { get; set; }

        /// <summary>
        /// Identifies when the reaction was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Reactors to the reaction subject with the emotion represented by this reaction group.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReactorConnection Reactors { get; set; }

        /// <summary>
        /// The subject that was reacted to.
        /// </summary>
        public IReactable Subject { get; set; }

        /// <summary>
        /// Users who have reacted to the reaction subject with the emotion represented by this reaction group
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReactingUserConnection Users { get; set; }

        /// <summary>
        /// Whether or not the authenticated user has left a reaction on the subject.
        /// </summary>
        public bool ViewerHasReacted { get; set; }
    }
}