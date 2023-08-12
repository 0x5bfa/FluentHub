// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which enterprise owners can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrgEnterpriseOwnerOrderField
	{
		/// <summary>
		/// Order enterprise owners by login.
		/// </summary>
		[EnumMember(Value = "LOGIN")]
		Login,
	}
}
