using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Classification of the advisory.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityAdvisoryClassification
    {
        /// <summary>
        /// Classification of general advisories.
        /// </summary>
        [EnumMember(Value = "GENERAL")]
        General,

        /// <summary>
        /// Classification of malware advisories.
        /// </summary>
        [EnumMember(Value = "MALWARE")]
        Malware,
    }
}