using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values for the enterprise members can create repositories setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseMembersCanCreateRepositoriesSettingValue
    {
        /// <summary>
        /// Organization owners choose whether to allow members to create repositories.
        /// </summary>
        [EnumMember(Value = "NO_POLICY")]
        NoPolicy,

        /// <summary>
        /// Members will be able to create public and private repositories.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// Members will be able to create only public repositories.
        /// </summary>
        [EnumMember(Value = "PUBLIC")]
        Public,

        /// <summary>
        /// Members will be able to create only private repositories.
        /// </summary>
        [EnumMember(Value = "PRIVATE")]
        Private,

        /// <summary>
        /// Members will not be able to create public or private repositories.
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,
    }
}