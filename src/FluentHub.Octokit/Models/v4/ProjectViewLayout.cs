using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The layout of a project view.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectViewLayout
    {
        /// <summary>
        /// Board layout
        /// </summary>
        [EnumMember(Value = "BOARD_LAYOUT")]
        BoardLayout,

        /// <summary>
        /// Table layout
        /// </summary>
        [EnumMember(Value = "TABLE_LAYOUT")]
        TableLayout,
    }
}