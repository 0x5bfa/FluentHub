// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The layout of a project v2 view.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2ViewLayout
	{
		/// <summary>
		/// Board layout
		/// </summary>
		[EnumMember(Value = "BOARD_LAYOUT")]
		BoardLayout,

		/// <summary>
		/// Table layout
		/// </summary>
		[EnumMember(Value = "TABLE_LAYOUT")]
		TableLayout,

		/// <summary>
		/// Roadmap layout
		/// </summary>
		[EnumMember(Value = "ROADMAP_LAYOUT")]
		RoadmapLayout,
	}
}
