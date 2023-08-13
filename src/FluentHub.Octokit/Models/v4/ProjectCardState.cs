// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Various content states of a ProjectCard
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectCardState
	{
		/// <summary>
		/// The card has content only.
		/// </summary>
		[EnumMember(Value = "CONTENT_ONLY")]
		ContentOnly,

		/// <summary>
		/// The card has a note only.
		/// </summary>
		[EnumMember(Value = "NOTE_ONLY")]
		NoteOnly,

		/// <summary>
		/// The card is redacted.
		/// </summary>
		[EnumMember(Value = "REDACTED")]
		Redacted,
	}
}
