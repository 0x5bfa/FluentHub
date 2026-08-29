using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a thread subscription form action
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ThreadSubscriptionFormAction
    {
        /// <summary>
        /// The User cannot subscribe or unsubscribe to the thread
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,

        /// <summary>
        /// The User can subscribe to the thread
        /// </summary>
        [EnumMember(Value = "SUBSCRIBE")]
        Subscribe,

        /// <summary>
        /// The User can unsubscribe to the thread
        /// </summary>
        [EnumMember(Value = "UNSUBSCRIBE")]
        Unsubscribe,
    }
}