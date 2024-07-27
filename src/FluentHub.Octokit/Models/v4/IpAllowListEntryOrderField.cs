// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which IP allow list entry connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IpAllowListEntryOrderField
	{
		/// <summary>
		/// Order IP allow list entries by creation time.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order IP allow list entries by the allow list value.
		/// </summary>
		[EnumMember(Value = "ALLOW_LIST_VALUE")]
		AllowListValue,
	}
}
