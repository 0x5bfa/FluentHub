// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An individual package
	/// </summary>
	public class SecurityAdvisoryPackage
	{
		/// <summary>
		/// The ecosystem the package belongs to, e.g. RUBYGEMS, NPM
		/// </summary>
		public SecurityAdvisoryEcosystem Ecosystem { get; set; }

		/// <summary>
		/// The package name
		/// </summary>
		public string Name { get; set; }
	}
}
