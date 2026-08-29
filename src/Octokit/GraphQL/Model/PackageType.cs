using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible types of a package.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PackageType
    {
        /// <summary>
        /// An npm package.
        /// </summary>
        [Obsolete(@"NPM will be removed from this enum as this type will be migrated to only be used by the Packages REST API. Removal on 2022-11-21 UTC.")]
        [EnumMember(Value = "NPM")]
        Npm,

        /// <summary>
        /// A rubygems package.
        /// </summary>
        [Obsolete(@"RUBYGEMS will be removed from this enum as this type will be migrated to only be used by the Packages REST API. Removal on 2022-12-28 UTC.")]
        [EnumMember(Value = "RUBYGEMS")]
        Rubygems,

        /// <summary>
        /// A maven package.
        /// </summary>
        [Obsolete(@"MAVEN will be removed from this enum as this type will be migrated to only be used by the Packages REST API. Removal on 2023-02-10 UTC.")]
        [EnumMember(Value = "MAVEN")]
        Maven,

        /// <summary>
        /// A docker image.
        /// </summary>
        [Obsolete(@"DOCKER will be removed from this enum as this type will be migrated to only be used by the Packages REST API. Removal on 2021-06-21 UTC.")]
        [EnumMember(Value = "DOCKER")]
        Docker,

        /// <summary>
        /// A debian package.
        /// </summary>
        [EnumMember(Value = "DEBIAN")]
        Debian,

        /// <summary>
        /// A nuget package.
        /// </summary>
        [Obsolete(@"NUGET will be removed from this enum as this type will be migrated to only be used by the Packages REST API. Removal on 2022-11-21 UTC.")]
        [EnumMember(Value = "NUGET")]
        Nuget,

        /// <summary>
        /// A python package.
        /// </summary>
        [EnumMember(Value = "PYPI")]
        Pypi,
    }
}