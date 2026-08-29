using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a subscription.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubscriptionState
    {
        /// <summary>
        /// The User is only notified when participating or @mentioned.
        /// </summary>
        [EnumMember(Value = "UNSUBSCRIBED")]
        Unsubscribed,

        /// <summary>
        /// The User is notified of all conversations.
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED")]
        Subscribed,

        /// <summary>
        /// The User is never notified.
        /// </summary>
        [EnumMember(Value = "IGNORED")]
        Ignored,
    }
}