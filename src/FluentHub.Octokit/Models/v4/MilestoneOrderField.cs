// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which milestone connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MilestoneOrderField
	{
		/// <summary>
		/// Order milestones by when they are due.
		/// </summary>
		[EnumMember(Value = "DUE_DATE")]
		DueDate,

		/// <summary>
		/// Order milestones by when they were created.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order milestones by when they were last updated.
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,

		/// <summary>
		/// Order milestones by their number.
		/// </summary>
		[EnumMember(Value = "NUMBER")]
		Number,
	}
}
