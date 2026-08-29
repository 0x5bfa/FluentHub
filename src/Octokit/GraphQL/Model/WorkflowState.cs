using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states for a workflow.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WorkflowState
    {
        /// <summary>
        /// The workflow is active.
        /// </summary>
        [EnumMember(Value = "ACTIVE")]
        Active,

        /// <summary>
        /// The workflow was deleted from the git repository.
        /// </summary>
        [EnumMember(Value = "DELETED")]
        Deleted,

        /// <summary>
        /// The workflow was disabled by default on a fork.
        /// </summary>
        [EnumMember(Value = "DISABLED_FORK")]
        DisabledFork,

        /// <summary>
        /// The workflow was disabled for inactivity in the repository.
        /// </summary>
        [EnumMember(Value = "DISABLED_INACTIVITY")]
        DisabledInactivity,

        /// <summary>
        /// The workflow was disabled manually.
        /// </summary>
        [EnumMember(Value = "DISABLED_MANUALLY")]
        DisabledManually,
    }
}