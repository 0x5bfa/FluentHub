using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values for an enabled/no policy enterprise setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseEnabledSettingValue
    {
        /// <summary>
        /// The setting is enabled for organizations in the enterprise.
        /// </summary>
        [EnumMember(Value = "ENABLED")]
        Enabled,

        /// <summary>
        /// There is no policy set for organizations in the enterprise.
        /// </summary>
        [EnumMember(Value = "NO_POLICY")]
        NoPolicy,
    }
}