// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which organization connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrganizationOrderField
	{
		/// <summary>
		/// Order organizations by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order organizations by login
		/// </summary>
		[EnumMember(Value = "LOGIN")]
		Login,
	}
}
