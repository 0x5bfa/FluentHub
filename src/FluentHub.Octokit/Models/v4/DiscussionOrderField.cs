// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which discussion connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DiscussionOrderField
	{
		/// <summary>
		/// Order discussions by creation time.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order discussions by most recent modification time.
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,
	}
}
