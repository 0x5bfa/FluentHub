// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The access level to a repository
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryPermission
	{
		/// <summary>
		/// Can read, clone, and push to this repository. Can also manage issues, pull requests, and repository settings, including adding collaborators
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,

		/// <summary>
		/// Can read, clone, and push to this repository. They can also manage issues, pull requests, and some repository settings
		/// </summary>
		[EnumMember(Value = "MAINTAIN")]
		Maintain,

		/// <summary>
		/// Can read, clone, and push to this repository. Can also manage issues and pull requests
		/// </summary>
		[EnumMember(Value = "WRITE")]
		Write,

		/// <summary>
		/// Can read and clone this repository. Can also manage issues and pull requests
		/// </summary>
		[EnumMember(Value = "TRIAGE")]
		Triage,

		/// <summary>
		/// Can read and clone this repository. Can also open and comment on issues and pull requests
		/// </summary>
		[EnumMember(Value = "READ")]
		Read,
	}
}
