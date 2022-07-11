namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UpdateCheckSuitePreferences
    /// </summary>
    public class UpdateCheckSuitePreferencesInput
    {
        /// <summary>
        /// The Node ID of the repository.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// The check suite preferences to modify.
        /// </summary>
        public IEnumerable<CheckSuiteAutoTriggerPreference> AutoTriggerPreferences { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}