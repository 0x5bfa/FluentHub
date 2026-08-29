using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The type of a project field.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2FieldType
    {
        /// <summary>
        /// Assignees
        /// </summary>
        [EnumMember(Value = "ASSIGNEES")]
        Assignees,

        /// <summary>
        /// Linked Pull Requests
        /// </summary>
        [EnumMember(Value = "LINKED_PULL_REQUESTS")]
        LinkedPullRequests,

        /// <summary>
        /// Reviewers
        /// </summary>
        [EnumMember(Value = "REVIEWERS")]
        Reviewers,

        /// <summary>
        /// Labels
        /// </summary>
        [EnumMember(Value = "LABELS")]
        Labels,

        /// <summary>
        /// Milestone
        /// </summary>
        [EnumMember(Value = "MILESTONE")]
        Milestone,

        /// <summary>
        /// Repository
        /// </summary>
        [EnumMember(Value = "REPOSITORY")]
        Repository,

        /// <summary>
        /// Title
        /// </summary>
        [EnumMember(Value = "TITLE")]
        Title,

        /// <summary>
        /// Text
        /// </summary>
        [EnumMember(Value = "TEXT")]
        Text,

        /// <summary>
        /// Single Select
        /// </summary>
        [EnumMember(Value = "SINGLE_SELECT")]
        SingleSelect,

        /// <summary>
        /// Number
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,

        /// <summary>
        /// Date
        /// </summary>
        [EnumMember(Value = "DATE")]
        Date,

        /// <summary>
        /// Iteration
        /// </summary>
        [EnumMember(Value = "ITERATION")]
        Iteration,

        /// <summary>
        /// Tracks
        /// </summary>
        [EnumMember(Value = "TRACKS")]
        Tracks,

        /// <summary>
        /// Tracked by
        /// </summary>
        [EnumMember(Value = "TRACKED_BY")]
        TrackedBy,
    }
}