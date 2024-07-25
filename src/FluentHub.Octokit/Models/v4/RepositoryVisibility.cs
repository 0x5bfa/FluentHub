// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The repository's visibility level.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryVisibility
	{
		/// <summary>
		/// The repository is visible only to those with explicit access.
		/// </summary>
		[EnumMember(Value = "PRIVATE")]
		Private,

		/// <summary>
		/// The repository is visible to everyone.
		/// </summary>
		[EnumMember(Value = "PUBLIC")]
		Public,

		/// <summary>
		/// The repository is visible only to users in the same business.
		/// </summary>
		[EnumMember(Value = "INTERNAL")]
		Internal,
	}
}
