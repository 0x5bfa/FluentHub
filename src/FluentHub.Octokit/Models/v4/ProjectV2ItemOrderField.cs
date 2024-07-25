// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which project v2 item connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2ItemOrderField
	{
		/// <summary>
		/// Order project v2 items by the their position in the project
		/// </summary>
		[EnumMember(Value = "POSITION")]
		Position,
	}
}
