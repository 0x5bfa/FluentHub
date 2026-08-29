using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states in which authentication can be configured with an identity provider.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IdentityProviderConfigurationState
    {
        /// <summary>
        /// Authentication with an identity provider is configured and enforced.
        /// </summary>
        [EnumMember(Value = "ENFORCED")]
        Enforced,

        /// <summary>
        /// Authentication with an identity provider is configured but not enforced.
        /// </summary>
        [EnumMember(Value = "CONFIGURED")]
        Configured,

        /// <summary>
        /// Authentication with an identity provider is not configured.
        /// </summary>
        [EnumMember(Value = "UNCONFIGURED")]
        Unconfigured,
    }
}