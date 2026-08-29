using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Identifier formats available for advisories.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityAdvisoryIdentifierType
    {
        /// <summary>
        /// Common Vulnerabilities and Exposures Identifier.
        /// </summary>
        [EnumMember(Value = "CVE")]
        Cve,

        /// <summary>
        /// GitHub Security Advisory ID.
        /// </summary>
        [EnumMember(Value = "GHSA")]
        Ghsa,
    }
}