using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible ecosystems of a dependency graph package.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DependencyGraphEcosystem
    {
        /// <summary>
        /// Ruby gems hosted at RubyGems.org
        /// </summary>
        [EnumMember(Value = "RUBYGEMS")]
        Rubygems,

        /// <summary>
        /// JavaScript packages hosted at npmjs.com
        /// </summary>
        [EnumMember(Value = "NPM")]
        Npm,

        /// <summary>
        /// Python packages hosted at PyPI.org
        /// </summary>
        [EnumMember(Value = "PIP")]
        Pip,

        /// <summary>
        /// Java artifacts hosted at the Maven central repository
        /// </summary>
        [EnumMember(Value = "MAVEN")]
        Maven,

        /// <summary>
        /// .NET packages hosted at the NuGet Gallery
        /// </summary>
        [EnumMember(Value = "NUGET")]
        Nuget,

        /// <summary>
        /// PHP packages hosted at packagist.org
        /// </summary>
        [EnumMember(Value = "COMPOSER")]
        Composer,

        /// <summary>
        /// Go modules
        /// </summary>
        [EnumMember(Value = "GO")]
        Go,

        /// <summary>
        /// GitHub Actions
        /// </summary>
        [EnumMember(Value = "ACTIONS")]
        Actions,

        /// <summary>
        /// Rust crates
        /// </summary>
        [EnumMember(Value = "RUST")]
        Rust,

        /// <summary>
        /// Dart packages hosted at pub.dev
        /// </summary>
        [EnumMember(Value = "PUB")]
        Pub,

        /// <summary>
        /// Swift packages
        /// </summary>
        [EnumMember(Value = "SWIFT")]
        Swift,
    }
}