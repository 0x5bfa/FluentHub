// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which user status connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserStatusOrderField
	{
		/// <summary>
		/// Order user statuses by when they were updated.
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,
	}
}
