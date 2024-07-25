// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which Enterprise Server user account email connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseServerUserAccountEmailOrderField
	{
		/// <summary>
		/// Order emails by email
		/// </summary>
		[EnumMember(Value = "EMAIL")]
		Email,
	}
}
