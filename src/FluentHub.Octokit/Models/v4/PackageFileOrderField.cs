using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
{
    /// <summary>
    /// Properties by which package file connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PackageFileOrderField
    {
        /// <summary>
        /// Order package files by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}