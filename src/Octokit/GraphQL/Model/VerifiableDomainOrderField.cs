using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which verifiable domain connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VerifiableDomainOrderField
    {
        /// <summary>
        /// Order verifiable domains by the domain name.
        /// </summary>
        [EnumMember(Value = "DOMAIN")]
        Domain,

        /// <summary>
        /// Order verifiable domains by their creation date.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}