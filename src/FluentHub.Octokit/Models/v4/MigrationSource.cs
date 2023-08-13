// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Enterprise Importer (GEI) migration source.
	/// </summary>
	public class MigrationSource
	{
		public ID Id { get; set; }

		/// <summary>
		/// The migration source name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The migration source type.
		/// </summary>
		public MigrationSourceType Type { get; set; }

		/// <summary>
		/// The migration source URL, for example `https://github.com` or `https://monalisa.ghe.com`.
		/// </summary>
		public string Url { get; set; }
	}
}
