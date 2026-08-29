using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible archived states of a project card.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectCardArchivedState
    {
        /// <summary>
        /// A project card that is archived
        /// </summary>
        [EnumMember(Value = "ARCHIVED")]
        Archived,

        /// <summary>
        /// A project card that is not archived
        /// </summary>
        [EnumMember(Value = "NOT_ARCHIVED")]
        NotArchived,
    }
}