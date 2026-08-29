using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The Octoshift Organization migration state.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationMigrationState
    {
        /// <summary>
        /// The Octoshift migration has not started.
        /// </summary>
        [EnumMember(Value = "NOT_STARTED")]
        NotStarted,

        /// <summary>
        /// The Octoshift migration has been queued.
        /// </summary>
        [EnumMember(Value = "QUEUED")]
        Queued,

        /// <summary>
        /// The Octoshift migration is in progress.
        /// </summary>
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,

        /// <summary>
        /// The Octoshift migration is performing pre repository migrations.
        /// </summary>
        [EnumMember(Value = "PRE_REPO_MIGRATION")]
        PreRepoMigration,

        /// <summary>
        /// The Octoshift org migration is performing repository migrations.
        /// </summary>
        [EnumMember(Value = "REPO_MIGRATION")]
        RepoMigration,

        /// <summary>
        /// The Octoshift migration is performing post repository migrations.
        /// </summary>
        [EnumMember(Value = "POST_REPO_MIGRATION")]
        PostRepoMigration,

        /// <summary>
        /// The Octoshift migration has succeeded.
        /// </summary>
        [EnumMember(Value = "SUCCEEDED")]
        Succeeded,

        /// <summary>
        /// The Octoshift migration has failed.
        /// </summary>
        [EnumMember(Value = "FAILED")]
        Failed,

        /// <summary>
        /// The Octoshift migration needs to have its credentials validated.
        /// </summary>
        [EnumMember(Value = "PENDING_VALIDATION")]
        PendingValidation,

        /// <summary>
        /// The Octoshift migration has invalid credentials.
        /// </summary>
        [EnumMember(Value = "FAILED_VALIDATION")]
        FailedValidation,
    }
}