using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible types of patch statuses.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PatchStatus
    {
        /// <summary>
        /// The file was added. Git status 'A'.
        /// </summary>
        [EnumMember(Value = "ADDED")]
        Added,

        /// <summary>
        /// The file was deleted. Git status 'D'.
        /// </summary>
        [EnumMember(Value = "DELETED")]
        Deleted,

        /// <summary>
        /// The file was renamed. Git status 'R'.
        /// </summary>
        [EnumMember(Value = "RENAMED")]
        Renamed,

        /// <summary>
        /// The file was copied. Git status 'C'.
        /// </summary>
        [EnumMember(Value = "COPIED")]
        Copied,

        /// <summary>
        /// The file's contents were changed. Git status 'M'.
        /// </summary>
        [EnumMember(Value = "MODIFIED")]
        Modified,

        /// <summary>
        /// The file's type was changed. Git status 'T'.
        /// </summary>
        [EnumMember(Value = "CHANGED")]
        Changed,
    }
}