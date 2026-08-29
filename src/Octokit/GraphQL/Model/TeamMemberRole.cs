using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible team member roles; either 'maintainer' or 'member'.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamMemberRole
    {
        /// <summary>
        /// A team maintainer has permission to add and remove team members.
        /// </summary>
        [EnumMember(Value = "MAINTAINER")]
        Maintainer,

        /// <summary>
        /// A team member has no administrative permissions on the team.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,
    }
}