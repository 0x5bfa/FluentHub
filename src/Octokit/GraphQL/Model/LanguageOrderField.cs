using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which language connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LanguageOrderField
    {
        /// <summary>
        /// Order languages by the size of all files containing the language
        /// </summary>
        [EnumMember(Value = "SIZE")]
        Size,
    }
}