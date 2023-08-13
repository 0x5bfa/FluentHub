// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository's open source license
	/// </summary>
	public class License
	{
		/// <summary>
		/// The full text of the license
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The conditions set by the license
		/// </summary>
		public List<LicenseRule> Conditions { get; set; }

		/// <summary>
		/// A human-readable description of the license
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Whether the license should be featured
		/// </summary>
		public bool Featured { get; set; }

		/// <summary>
		/// Whether the license should be displayed in license pickers
		/// </summary>
		public bool Hidden { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Instructions on how to implement the license
		/// </summary>
		public string Implementation { get; set; }

		/// <summary>
		/// The lowercased SPDX ID of the license
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// The limitations set by the license
		/// </summary>
		public List<LicenseRule> Limitations { get; set; }

		/// <summary>
		/// The license full name specified by &lt;https://spdx.org/licenses&gt;
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Customary short name if applicable (e.g, GPLv3)
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// The permissions set by the license
		/// </summary>
		public List<LicenseRule> Permissions { get; set; }

		/// <summary>
		/// Whether the license is a pseudo-license placeholder (e.g., other, no-license)
		/// </summary>
		public bool PseudoLicense { get; set; }

		/// <summary>
		/// Short identifier specified by &lt;https://spdx.org/licenses&gt;
		/// </summary>
		public string SpdxId { get; set; }

		/// <summary>
		/// URL to the license on &lt;https://choosealicense.com&gt;
		/// </summary>
		public string Url { get; set; }
	}
}
