// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Email attributes from External Identity
	/// </summary>
	public class UserEmailMetadata
	{
		/// <summary>
		/// Boolean to identify primary emails
		/// </summary>
		public bool? Primary { get; set; }

		/// <summary>
		/// Type of email
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Email id
		/// </summary>
		public string Value { get; set; }
	}
}
