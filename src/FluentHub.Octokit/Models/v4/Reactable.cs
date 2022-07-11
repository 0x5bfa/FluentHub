namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a subject that can be reacted on.
    /// </summary>
    public interface IReactable
    {
        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        int? DatabaseId { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        IQueryableList<ReactionGroup> ReactionGroups { get; set; }

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        ReactionConnection Reactions { get; set; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        bool ViewerCanReact { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIReactable
    {
        

        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        public IQueryableList<ReactionGroup> ReactionGroups { get; set; }

        public ReactionConnection Reactions { get; set; }

        public bool ViewerCanReact { get; set; }
    }
}