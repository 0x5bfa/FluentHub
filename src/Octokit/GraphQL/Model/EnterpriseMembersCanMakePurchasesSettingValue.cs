using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values for the members can make purchases setting.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseMembersCanMakePurchasesSettingValue
    {
        /// <summary>
        /// The setting is enabled for organizations in the enterprise.
        /// </summary>
        [EnumMember(Value = "ENABLED")]
        Enabled,

        /// <summary>
        /// The setting is disabled for organizations in the enterprise.
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,
    }
}