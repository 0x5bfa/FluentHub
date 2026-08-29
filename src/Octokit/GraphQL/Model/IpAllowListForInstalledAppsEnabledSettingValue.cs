using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values for the IP allow list configuration for installed GitHub Apps setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IpAllowListForInstalledAppsEnabledSettingValue
    {
        /// <summary>
        /// The setting is enabled for the owner.
        /// </summary>
        [EnumMember(Value = "ENABLED")]
        Enabled,

        /// <summary>
        /// The setting is disabled for the owner.
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,
    }
}