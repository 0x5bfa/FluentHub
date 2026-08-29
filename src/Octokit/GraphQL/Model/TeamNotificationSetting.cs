using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible team notification values.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamNotificationSetting
    {
        /// <summary>
        /// Everyone will receive notifications when the team is @mentioned.
        /// </summary>
        [EnumMember(Value = "NOTIFICATIONS_ENABLED")]
        NotificationsEnabled,

        /// <summary>
        /// No one will receive notifications.
        /// </summary>
        [EnumMember(Value = "NOTIFICATIONS_DISABLED")]
        NotificationsDisabled,
    }
}