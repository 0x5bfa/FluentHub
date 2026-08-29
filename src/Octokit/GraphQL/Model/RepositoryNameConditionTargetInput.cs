namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parameters to be used for the repository_name condition
    /// </summary>
    public class RepositoryNameConditionTargetInput
    {
        /// <summary>
        /// Array of repository names or patterns to exclude. The condition will not pass if any of these patterns match.
        /// </summary>
        public IEnumerable<string> Exclude { get; set; }

        /// <summary>
        /// Array of repository names or patterns to include. One of these patterns must match for the condition to pass. Also accepts `~ALL` to include all repositories.
        /// </summary>
        public IEnumerable<string> Include { get; set; }

        /// <summary>
        /// Target changes that match these patterns will be prevented except by those with bypass permissions.
        /// </summary>
        public bool? Protected { get; set; }
    }
}