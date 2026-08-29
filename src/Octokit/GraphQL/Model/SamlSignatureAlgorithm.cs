using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible signature algorithms used to sign SAML requests for a Identity Provider.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SamlSignatureAlgorithm
    {
        /// <summary>
        /// RSA-SHA1
        /// </summary>
        [EnumMember(Value = "RSA_SHA1")]
        RsaSha1,

        /// <summary>
        /// RSA-SHA256
        /// </summary>
        [EnumMember(Value = "RSA_SHA256")]
        RsaSha256,

        /// <summary>
        /// RSA-SHA384
        /// </summary>
        [EnumMember(Value = "RSA_SHA384")]
        RsaSha384,

        /// <summary>
        /// RSA-SHA512
        /// </summary>
        [EnumMember(Value = "RSA_SHA512")]
        RsaSha512,
    }
}