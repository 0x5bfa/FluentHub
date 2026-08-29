using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The OIDC identity provider type
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OIDCProviderType
    {
        /// <summary>
        /// Azure Active Directory
        /// </summary>
        [EnumMember(Value = "AAD")]
        Aad,
    }
}