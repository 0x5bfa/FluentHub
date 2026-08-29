using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which mannequins can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MannequinOrderField
    {
        /// <summary>
        /// Order mannequins alphabetically by their source login.
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Order mannequins why when they were created.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}