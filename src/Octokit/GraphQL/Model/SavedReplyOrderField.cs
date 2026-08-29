using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which saved reply connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SavedReplyOrderField
    {
        /// <summary>
        /// Order saved reply by when they were updated.
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,
    }
}