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
    public enum ThreadSubscriptionState
    {
        /// <summary>
        /// The subscription status is currently unavailable.
        /// </summary>
        [EnumMember(Value = "UNAVAILABLE")]
        Unavailable,

        /// <summary>
        /// The subscription status is currently disabled.
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,

        /// <summary>
        /// The User is never notified because they are ignoring the list
        /// </summary>
        [EnumMember(Value = "IGNORING_LIST")]
        IgnoringList,

        /// <summary>
        /// The User is notified because they chose custom settings for this thread.
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED_TO_THREAD_EVENTS")]
        SubscribedToThreadEvents,

        /// <summary>
        /// The User is never notified because they are ignoring the thread
        /// </summary>
        [EnumMember(Value = "IGNORING_THREAD")]
        IgnoringThread,

        /// <summary>
        /// The User is notified becuase they are watching the list
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED_TO_LIST")]
        SubscribedToList,

        /// <summary>
        /// The User is notified because they chose custom settings for this thread.
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED_TO_THREAD_TYPE")]
        SubscribedToThreadType,

        /// <summary>
        /// The User is notified because they are subscribed to the thread
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED_TO_THREAD")]
        SubscribedToThread,

        /// <summary>
        /// The User is not recieving notifications from this thread
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,
    }
}