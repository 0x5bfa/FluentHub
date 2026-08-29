using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which enterprise connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseOrderField
    {
        /// <summary>
        /// Order enterprises by name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,
    }
}