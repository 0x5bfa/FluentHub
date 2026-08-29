using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible roles of a collaborator on a project.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2Roles
    {
        /// <summary>
        /// The collaborator has no direct access to the project
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,

        /// <summary>
        /// The collaborator can view the project
        /// </summary>
        [EnumMember(Value = "READER")]
        Reader,

        /// <summary>
        /// The collaborator can view and edit the project
        /// </summary>
        [EnumMember(Value = "WRITER")]
        Writer,

        /// <summary>
        /// The collaborator can view, edit, and maange the settings of the project
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,
    }
}