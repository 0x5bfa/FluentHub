using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The corresponding operation type for the action
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationType
    {
        /// <summary>
        /// An existing resource was accessed
        /// </summary>
        [EnumMember(Value = "ACCESS")]
        Access,

        /// <summary>
        /// A resource performed an authentication event
        /// </summary>
        [EnumMember(Value = "AUTHENTICATION")]
        Authentication,

        /// <summary>
        /// A new resource was created
        /// </summary>
        [EnumMember(Value = "CREATE")]
        Create,

        /// <summary>
        /// An existing resource was modified
        /// </summary>
        [EnumMember(Value = "MODIFY")]
        Modify,

        /// <summary>
        /// An existing resource was removed
        /// </summary>
        [EnumMember(Value = "REMOVE")]
        Remove,

        /// <summary>
        /// An existing resource was restored
        /// </summary>
        [EnumMember(Value = "RESTORE")]
        Restore,

        /// <summary>
        /// An existing resource was transferred between multiple resources
        /// </summary>
        [EnumMember(Value = "TRANSFER")]
        Transfer,
    }
}