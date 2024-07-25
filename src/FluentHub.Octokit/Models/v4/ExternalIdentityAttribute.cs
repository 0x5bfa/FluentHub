// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An attribute for the External Identity attributes collection
	/// </summary>
	public class ExternalIdentityAttribute
	{
		/// <summary>
		/// The attribute metadata as JSON
		/// </summary>
		public string Metadata { get; set; }

		/// <summary>
		/// The attribute name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The attribute value
		/// </summary>
		public string Value { get; set; }
	}
}
