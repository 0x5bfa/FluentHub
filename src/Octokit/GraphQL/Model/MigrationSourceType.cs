using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Represents the different GitHub Enterprise Importer (GEI) migration sources.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MigrationSourceType
    {
        /// <summary>
        /// An Azure DevOps migration source.
        /// </summary>
        [EnumMember(Value = "AZURE_DEVOPS")]
        AzureDevops,

        /// <summary>
        /// A Bitbucket Server migration source.
        /// </summary>
        [EnumMember(Value = "BITBUCKET_SERVER")]
        BitbucketServer,

        /// <summary>
        /// A GitHub Migration API source.
        /// </summary>
        [EnumMember(Value = "GITHUB_ARCHIVE")]
        GithubArchive,
    }
}