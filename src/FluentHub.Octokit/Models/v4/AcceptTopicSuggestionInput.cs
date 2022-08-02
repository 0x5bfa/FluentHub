namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of AcceptTopicSuggestion
    /// </summary>
    public class AcceptTopicSuggestionInput
    {        /// <summary>
        /// The Node ID of the repository.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// The name of the suggested topic.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}