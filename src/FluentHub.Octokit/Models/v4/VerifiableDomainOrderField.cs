// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which verifiable domain connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum VerifiableDomainOrderField
	{
		/// <summary>
		/// Order verifiable domains by the domain name.
		/// </summary>
		[EnumMember(Value = "DOMAIN")]
		Domain,

		/// <summary>
		/// Order verifiable domains by their creation date.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
