// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for environments
	/// </summary>
	public class Environments
	{
		/// <summary>
		/// The field to order environments by.
		/// </summary>
		public EnvironmentOrderField Field { get; set; }

		/// <summary>
		/// The direction in which to order environments by the specified field.
		/// </summary>
		public OrderDirection Direction { get; set; }
	}
}
