// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The filters that are available when fetching check runs.
	/// </summary>
	public class CheckRunFilter
	{
		/// <summary>
		/// Filters the check runs by this type.
		/// </summary>
		public CheckRunType? CheckType { get; set; }

		/// <summary>
		/// Filters the check runs created by this application ID.
		/// </summary>
		public int? AppId { get; set; }

		/// <summary>
		/// Filters the check runs by this name.
		/// </summary>
		public string CheckName { get; set; }

		/// <summary>
		/// Filters the check runs by this status. Superceded by statuses.
		/// </summary>
		public CheckStatusState? Status { get; set; }

		/// <summary>
		/// Filters the check runs by this status. Overrides status.
		/// </summary>
		public List<CheckStatusState> Statuses { get; set; }

		/// <summary>
		/// Filters the check runs by these conclusions.
		/// </summary>
		public List<CheckConclusionState> Conclusions { get; set; }
	}
}
