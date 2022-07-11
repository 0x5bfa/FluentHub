namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UpdateProjectDraftIssue
    /// </summary>
    public class UpdateProjectDraftIssueInput
    {
        /// <summary>
        /// The ID of the draft issue to update.
        /// </summary>
        public ID DraftIssueId { get; set; }

        /// <summary>
        /// The title of the draft issue.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The body of the draft issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The IDs of the assignees of the draft issue.
        /// </summary>
        public IEnumerable<ID> AssigneeIds { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}