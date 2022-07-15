using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The possible values for the notification restriction setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationRestrictionSettingValue
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