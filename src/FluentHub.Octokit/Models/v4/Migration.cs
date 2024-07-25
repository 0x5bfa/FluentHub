// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents a GitHub Enterprise Importer (GEI) migration.
	/// </summary>
	public interface IMigration
	{
		/// <summary>
		/// The migration flag to continue on error.
		/// </summary>
		bool ContinueOnError { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		string DatabaseId { get; set; }

		/// <summary>
		/// The reason the migration failed.
		/// </summary>
		string FailureReason { get; set; }

		/// <summary>
		/// The Node ID of the Migration object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// The URL for the migration log (expires 1 day after migration completes).
		/// </summary>
		string MigrationLogUrl { get; set; }

		/// <summary>
		/// The migration source.
		/// </summary>
		MigrationSource MigrationSource { get; set; }

		/// <summary>
		/// The target repository name.
		/// </summary>
		string RepositoryName { get; set; }

		/// <summary>
		/// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
		/// </summary>
		string SourceUrl { get; set; }

		/// <summary>
		/// The migration state.
		/// </summary>
		MigrationState State { get; set; }

		/// <summary>
		/// The number of warnings encountered for this migration. To review the warnings, check the [Migration Log](https://docs.github.com/migrations/using-github-enterprise-importer/completing-your-migration-with-github-enterprise-importer/accessing-your-migration-logs-for-github-enterprise-importer).
		/// </summary>
		int WarningsCount { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Migration : IMigration
	{
		public bool ContinueOnError { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public string CreatedAtHumanized { get; set; }

		public string DatabaseId { get; set; }

		public string FailureReason { get; set; }

		public ID Id { get; set; }

		public string MigrationLogUrl { get; set; }

		public MigrationSource MigrationSource { get; set; }

		public string RepositoryName { get; set; }

		public string SourceUrl { get; set; }

		public MigrationState State { get; set; }

		public int WarningsCount { get; set; }
	}
}

