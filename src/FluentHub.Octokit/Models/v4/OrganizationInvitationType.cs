// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible organization invitation types.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrganizationInvitationType
	{
		/// <summary>
		/// The invitation was to an existing user.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,

		/// <summary>
		/// The invitation was to an email address.
		/// </summary>
		[EnumMember(Value = "EMAIL")]
		Email,
	}
}
