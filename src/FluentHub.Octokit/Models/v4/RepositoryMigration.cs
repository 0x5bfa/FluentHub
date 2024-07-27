// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Enterprise Importer (GEI) repository migration.
	/// </summary>
	public class RepositoryMigration
	{
		/// <summary>
		/// The migration flag to continue on error.
		/// </summary>
		public bool ContinueOnError { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public string DatabaseId { get; set; }

		/// <summary>
		/// The reason the migration failed.
		/// </summary>
		public string FailureReason { get; set; }

		/// <summary>
		/// The Node ID of the RepositoryMigration object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The URL for the migration log (expires 1 day after migration completes).
		/// </summary>
		public string MigrationLogUrl { get; set; }

		/// <summary>
		/// The migration source.
		/// </summary>
		public MigrationSource MigrationSource { get; set; }

		/// <summary>
		/// The target repository name.
		/// </summary>
		public string RepositoryName { get; set; }

		/// <summary>
		/// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
		/// </summary>
		public string SourceUrl { get; set; }

		/// <summary>
		/// The migration state.
		/// </summary>
		public MigrationState State { get; set; }

		/// <summary>
		/// The number of warnings encountered for this migration. To review the warnings, check the [Migration Log](https://docs.github.com/migrations/using-github-enterprise-importer/completing-your-migration-with-github-enterprise-importer/accessing-your-migration-logs-for-github-enterprise-importer).
		/// </summary>
		public int WarningsCount { get; set; }
	}
}
