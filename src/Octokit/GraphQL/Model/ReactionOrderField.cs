using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// A list of fields that reactions can be ordered by.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReactionOrderField
    {
        /// <summary>
        /// Allows ordering a list of reactions by when they were created.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}