namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CreateProject
    /// </summary>
    public class CreateProjectInput
    {
        /// <summary>
        /// The owner ID to create the project under.
        /// </summary>
        public ID OwnerId { get; set; }

        /// <summary>
        /// The name of project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of project.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The name of the GitHub-provided template.
        /// </summary>
        public ProjectTemplate? Template { get; set; }

        /// <summary>
        /// A list of repository IDs to create as linked repositories for the project
        /// </summary>
        public IEnumerable<ID> RepositoryIds { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}