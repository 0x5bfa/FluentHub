// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The permissions available for repository creation on an Organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrgUpdateMemberRepositoryCreationPermissionAuditEntryVisibility
	{
		/// <summary>
		/// All organization members are restricted from creating any repositories.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,

		/// <summary>
		/// All organization members are restricted from creating public repositories.
		/// </summary>
		[EnumMember(Value = "PUBLIC")]
		Public,

		/// <summary>
		/// All organization members are allowed to create any repositories.
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,

		/// <summary>
		/// All organization members are restricted from creating private repositories.
		/// </summary>
		[EnumMember(Value = "PRIVATE")]
		Private,

		/// <summary>
		/// All organization members are restricted from creating internal repositories.
		/// </summary>
		[EnumMember(Value = "INTERNAL")]
		Internal,

		/// <summary>
		/// All organization members are restricted from creating public or internal repositories.
		/// </summary>
		[EnumMember(Value = "PUBLIC_INTERNAL")]
		PublicInternal,

		/// <summary>
		/// All organization members are restricted from creating private or internal repositories.
		/// </summary>
		[EnumMember(Value = "PRIVATE_INTERNAL")]
		PrivateInternal,

		/// <summary>
		/// All organization members are restricted from creating public or private repositories.
		/// </summary>
		[EnumMember(Value = "PUBLIC_PRIVATE")]
		PublicPrivate,
	}
}
