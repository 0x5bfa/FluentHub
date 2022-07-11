using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
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