using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible ecosystems of a security vulnerability's package.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityAdvisoryEcosystem
    {
        /// <summary>
        /// PHP packages hosted at packagist.org
        /// </summary>
        [EnumMember(Value = "COMPOSER")]
        Composer,

        /// <summary>
        /// Erlang/Elixir packages hosted at hex.pm
        /// </summary>
        [EnumMember(Value = "ERLANG")]
        Erlang,

        /// <summary>
        /// GitHub Actions
        /// </summary>
        [EnumMember(Value = "ACTIONS")]
        Actions,

        /// <summary>
        /// Go modules
        /// </summary>
        [EnumMember(Value = "GO")]
        Go,

        /// <summary>
        /// Java artifacts hosted at the Maven central repository
        /// </summary>
        [EnumMember(Value = "MAVEN")]
        Maven,

        /// <summary>
        /// JavaScript packages hosted at npmjs.com
        /// </summary>
        [EnumMember(Value = "NPM")]
        Npm,

        /// <summary>
        /// .NET packages hosted at the NuGet Gallery
        /// </summary>
        [EnumMember(Value = "NUGET")]
        Nuget,

        /// <summary>
        /// Python packages hosted at PyPI.org
        /// </summary>
        [EnumMember(Value = "PIP")]
        Pip,

        /// <summary>
        /// Dart packages hosted at pub.dev
        /// </summary>
        [EnumMember(Value = "PUB")]
        Pub,

        /// <summary>
        /// Ruby gems hosted at RubyGems.org
        /// </summary>
        [EnumMember(Value = "RUBYGEMS")]
        Rubygems,

        /// <summary>
        /// Rust crates
        /// </summary>
        [EnumMember(Value = "RUST")]
        Rust,

        /// <summary>
        /// Swift packages
        /// </summary>
        [EnumMember(Value = "SWIFT")]
        Swift,
    }
}