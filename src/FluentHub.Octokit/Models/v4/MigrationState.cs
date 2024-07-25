// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The GitHub Enterprise Importer (GEI) migration state.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MigrationState
	{
		/// <summary>
		/// The migration has not started.
		/// </summary>
		[EnumMember(Value = "NOT_STARTED")]
		NotStarted,

		/// <summary>
		/// The migration has been queued.
		/// </summary>
		[EnumMember(Value = "QUEUED")]
		Queued,

		/// <summary>
		/// The migration is in progress.
		/// </summary>
		[EnumMember(Value = "IN_PROGRESS")]
		InProgress,

		/// <summary>
		/// The migration has succeeded.
		/// </summary>
		[EnumMember(Value = "SUCCEEDED")]
		Succeeded,

		/// <summary>
		/// The migration has failed.
		/// </summary>
		[EnumMember(Value = "FAILED")]
		Failed,

		/// <summary>
		/// The migration needs to have its credentials validated.
		/// </summary>
		[EnumMember(Value = "PENDING_VALIDATION")]
		PendingValidation,

		/// <summary>
		/// The migration has invalid credentials.
		/// </summary>
		[EnumMember(Value = "FAILED_VALIDATION")]
		FailedValidation,
	}
}
