// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An advisory identifier to filter results on.
	/// </summary>
	public class SecurityAdvisoryIdentifierFilter
	{
		/// <summary>
		/// The identifier type.
		/// </summary>
		public SecurityAdvisoryIdentifierType Type { get; set; }

		/// <summary>
		/// The identifier string. Supports exact or partial matching.
		/// </summary>
		public string Value { get; set; }
	}
}
