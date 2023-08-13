// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
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
