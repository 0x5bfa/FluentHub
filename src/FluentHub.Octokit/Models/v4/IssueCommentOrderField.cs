// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which issue comment connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IssueCommentOrderField
	{
		/// <summary>
		/// Order issue comments by update time
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,
	}
}
