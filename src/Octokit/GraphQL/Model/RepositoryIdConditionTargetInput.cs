namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parameters to be used for the repository_id condition
    /// </summary>
    public class RepositoryIdConditionTargetInput
    {
        /// <summary>
        /// One of these repo IDs must match the repo.
        /// </summary>
        public IEnumerable<ID> RepositoryIds { get; set; }
    }
}