// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Collaborators affiliation level with a subject.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CollaboratorAffiliation
	{
		/// <summary>
		/// All outside collaborators of an organization-owned subject.
		/// </summary>
		[EnumMember(Value = "OUTSIDE")]
		Outside,

		/// <summary>
		/// All collaborators with permissions to an organization-owned subject, regardless of organization membership status.
		/// </summary>
		[EnumMember(Value = "DIRECT")]
		Direct,

		/// <summary>
		/// All collaborators the authenticated user can see.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,
	}
}
