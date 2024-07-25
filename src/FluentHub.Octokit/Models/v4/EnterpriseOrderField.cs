// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which enterprise connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseOrderField
	{
		/// <summary>
		/// Order enterprises by name
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,
	}
}
