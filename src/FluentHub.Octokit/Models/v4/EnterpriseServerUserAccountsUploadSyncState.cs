// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Synchronization state of the Enterprise Server user accounts upload
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseServerUserAccountsUploadSyncState
	{
		/// <summary>
		/// The synchronization of the upload is pending.
		/// </summary>
		[EnumMember(Value = "PENDING")]
		Pending,

		/// <summary>
		/// The synchronization of the upload succeeded.
		/// </summary>
		[EnumMember(Value = "SUCCESS")]
		Success,

		/// <summary>
		/// The synchronization of the upload failed.
		/// </summary>
		[EnumMember(Value = "FAILURE")]
		Failure,
	}
}
