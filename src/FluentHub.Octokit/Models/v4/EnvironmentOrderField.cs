// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which environments connections can be ordered
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnvironmentOrderField
	{
		/// <summary>
		/// Order environments by name.
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,
	}
}
