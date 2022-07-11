using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Represents the different Octoshift migration sources.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MigrationSourceType
    {
        /// <summary>
        /// A GitLab migration source.
        /// </summary>
        [EnumMember(Value = "GITLAB")]
        Gitlab,

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
        /// A GitHub migration source.
        /// </summary>
        [EnumMember(Value = "GITHUB")]
        Github,

        /// <summary>
        /// A GitHub Migration API source.
        /// </summary>
        [EnumMember(Value = "GITHUB_ARCHIVE")]
        GithubArchive,
    }
}