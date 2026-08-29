namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Parameters to be used for the repository_property condition
    /// </summary>
    public class RepositoryPropertyConditionTargetInput
    {
        /// <summary>
        /// Array of repository properties that must not match.
        /// </summary>
        public IEnumerable<PropertyTargetDefinitionInput> Exclude { get; set; }

        /// <summary>
        /// Array of repository properties that must match
        /// </summary>
        public IEnumerable<PropertyTargetDefinitionInput> Include { get; set; }
    }
}