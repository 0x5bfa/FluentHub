// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The privacy of a repository
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryPrivacy
	{
		/// <summary>
		/// Public
		/// </summary>
		[EnumMember(Value = "PUBLIC")]
		Public,

		/// <summary>
		/// Private
		/// </summary>
		[EnumMember(Value = "PRIVATE")]
		Private,
	}
}
