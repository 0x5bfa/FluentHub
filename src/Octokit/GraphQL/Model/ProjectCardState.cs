using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Various content states of a ProjectCard
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectCardState
    {
        /// <summary>
        /// The card has content only.
        /// </summary>
        [EnumMember(Value = "CONTENT_ONLY")]
        ContentOnly,

        /// <summary>
        /// The card has a note only.
        /// </summary>
        [EnumMember(Value = "NOTE_ONLY")]
        NoteOnly,

        /// <summary>
        /// The card is redacted.
        /// </summary>
        [EnumMember(Value = "REDACTED")]
        Redacted,
    }
}