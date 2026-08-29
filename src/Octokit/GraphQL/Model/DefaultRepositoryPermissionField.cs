using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible base permissions for repositories.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DefaultRepositoryPermissionField
    {
        /// <summary>
        /// No access
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,

        /// <summary>
        /// Can read repos by default
        /// </summary>
        [EnumMember(Value = "READ")]
        Read,

        /// <summary>
        /// Can read and write repos by default
        /// </summary>
        [EnumMember(Value = "WRITE")]
        Write,

        /// <summary>
        /// Can read, write, and administrate repos by default
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,
    }
}