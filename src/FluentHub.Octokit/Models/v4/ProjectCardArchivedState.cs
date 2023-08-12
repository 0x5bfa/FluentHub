// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible archived states of a project card.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectCardArchivedState
	{
		/// <summary>
		/// A project card that is archived
		/// </summary>
		[EnumMember(Value = "ARCHIVED")]
		Archived,

		/// <summary>
		/// A project card that is not archived
		/// </summary>
		[EnumMember(Value = "NOT_ARCHIVED")]
		NotArchived,
	}
}
