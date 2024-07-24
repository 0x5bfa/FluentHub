// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which ref connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RefOrderField
	{
		/// <summary>
		/// Order refs by underlying commit date if the ref prefix is refs/tags/
		/// </summary>
		[EnumMember(Value = "TAG_COMMIT_DATE")]
		TagCommitDate,

		/// <summary>
		/// Order refs by their alphanumeric name
		/// </summary>
		[EnumMember(Value = "ALPHABETICAL")]
		Alphabetical,
	}
}
