namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A message to include with a new commit
    /// </summary>
    public class CommitMessage
    {
        /// <summary>
        /// The headline of the message.
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// The body of the message.
        /// </summary>
        public string Body { get; set; }
    }
}