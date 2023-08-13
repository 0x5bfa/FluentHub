// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which Enterprise Server installation connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseServerInstallationOrderField
	{
		/// <summary>
		/// Order Enterprise Server installations by host name
		/// </summary>
		[EnumMember(Value = "HOST_NAME")]
		HostName,

		/// <summary>
		/// Order Enterprise Server installations by customer name
		/// </summary>
		[EnumMember(Value = "CUSTOMER_NAME")]
		CustomerName,

		/// <summary>
		/// Order Enterprise Server installations by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
