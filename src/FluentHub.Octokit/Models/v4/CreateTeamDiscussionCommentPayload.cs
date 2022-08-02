namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Autogenerated return type of CreateTeamDiscussionComment
    /// </summary>
    public class CreateTeamDiscussionCommentPayload
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The new comment.
        /// </summary>
        public TeamDiscussionComment TeamDiscussionComment { get; set; }
    }
}