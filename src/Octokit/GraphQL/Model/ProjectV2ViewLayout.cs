using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The layout of a project v2 view.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2ViewLayout
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

        /// <summary>
        /// Roadmap layout
        /// </summary>
        [EnumMember(Value = "ROADMAP_LAYOUT")]
        RoadmapLayout,
    }
}