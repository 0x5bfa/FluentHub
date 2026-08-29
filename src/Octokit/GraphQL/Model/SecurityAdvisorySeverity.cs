using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Severity of the vulnerability.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityAdvisorySeverity
    {
        /// <summary>
        /// Low.
        /// </summary>
        [EnumMember(Value = "LOW")]
        Low,

        /// <summary>
        /// Moderate.
        /// </summary>
        [EnumMember(Value = "MODERATE")]
        Moderate,

        /// <summary>
        /// High.
        /// </summary>
        [EnumMember(Value = "HIGH")]
        High,

        /// <summary>
        /// Critical.
        /// </summary>
        [EnumMember(Value = "CRITICAL")]
        Critical,
    }
}