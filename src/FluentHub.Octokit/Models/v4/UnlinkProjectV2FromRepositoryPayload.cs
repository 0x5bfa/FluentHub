namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Autogenerated return type of UnlinkProjectV2FromRepository
    /// </summary>
    public class UnlinkProjectV2FromRepositoryPayload
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The repository the project is no longer linked to.
        /// </summary>
        public Repository Repository { get; set; }
    }
}