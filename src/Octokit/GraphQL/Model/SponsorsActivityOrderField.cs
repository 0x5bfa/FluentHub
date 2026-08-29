using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which GitHub Sponsors activity connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsActivityOrderField
    {
        /// <summary>
        /// Order activities by when they happened.
        /// </summary>
        [EnumMember(Value = "TIMESTAMP")]
        Timestamp,
    }
}