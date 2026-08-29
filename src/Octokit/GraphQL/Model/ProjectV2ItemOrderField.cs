using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which project v2 item connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2ItemOrderField
    {
        /// <summary>
        /// Order project v2 items by the their position in the project
        /// </summary>
        [EnumMember(Value = "POSITION")]
        Position,
    }
}