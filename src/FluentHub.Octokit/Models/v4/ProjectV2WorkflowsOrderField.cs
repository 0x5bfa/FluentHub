// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which project workflows can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2WorkflowsOrderField
	{
		/// <summary>
		/// The name of the workflow
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,

		/// <summary>
		/// The number of the workflow
		/// </summary>
		[EnumMember(Value = "NUMBER")]
		Number,

		/// <summary>
		/// The date and time of the workflow update
		/// </summary>
		[EnumMember(Value = "UPDATED_AT")]
		UpdatedAt,

		/// <summary>
		/// The date and time of the workflow creation
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
