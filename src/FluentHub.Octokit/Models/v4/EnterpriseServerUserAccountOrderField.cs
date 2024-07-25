// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which Enterprise Server user account connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseServerUserAccountOrderField
	{
		/// <summary>
		/// Order user accounts by login
		/// </summary>
		[EnumMember(Value = "LOGIN")]
		Login,

		/// <summary>
		/// Order user accounts by creation time on the Enterprise Server installation
		/// </summary>
		[EnumMember(Value = "REMOTE_CREATED_AT")]
		RemoteCreatedAt,
	}
}
