namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Saved Reply is text a user can use to reply quickly.
    /// </summary>
    public class SavedReply
    {
        /// <summary>
        /// The body of the saved reply.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The saved reply body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The title of the saved reply.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The user that saved this reply.
        /// </summary>
        public IActor User { get; set; }
    }
}