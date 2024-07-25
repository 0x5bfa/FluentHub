// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which security advisory connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SecurityAdvisoryOrderField
	{
		/// <summary>
		/// Order advisories by publication time
		/// </summary>
		[EnumMember(Value = "PUBLISHED_AT")]
		PublishedAt,

		/// <summary>
		/// Order advisories by update time
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,
	}
}
