namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of MoveProjectCard
    /// </summary>
    public class MoveProjectCardInput
    {        /// <summary>
        /// The id of the card to move.
        /// </summary>
        public ID CardId { get; set; }

        /// <summary>
        /// The id of the column to move it into.
        /// </summary>
        public ID ColumnId { get; set; }

        /// <summary>
        /// Place the new card after the card with this id. Pass null to place it at the top.
        /// </summary>
        public ID? AfterCardId { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}