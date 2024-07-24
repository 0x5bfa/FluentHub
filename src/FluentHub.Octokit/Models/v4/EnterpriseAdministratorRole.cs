// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible administrator roles in an enterprise account.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseAdministratorRole
	{
		/// <summary>
		/// Represents an owner of the enterprise account.
		/// </summary>
		[EnumMember(Value = "OWNER")]
		Owner,

		/// <summary>
		/// Represents a billing manager of the enterprise account.
		/// </summary>
		[EnumMember(Value = "BILLING_MANAGER")]
		BillingManager,
	}
}
