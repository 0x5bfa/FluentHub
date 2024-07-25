// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Ordering options for repository migrations.
	/// </summary>
	public class RepositoryMigrationOrder
	{
		/// <summary>
		/// The field to order repository migrations by.
		/// </summary>
		public RepositoryMigrationOrderField Field { get; set; }

		/// <summary>
		/// The ordering direction.
		/// </summary>
		public RepositoryMigrationOrderDirection Direction { get; set; }
	}
}
