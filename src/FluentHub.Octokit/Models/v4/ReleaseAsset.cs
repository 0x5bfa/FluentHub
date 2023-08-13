// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A release asset contains the content for a release asset.
	/// </summary>
	public class ReleaseAsset
	{
		/// <summary>
		/// The asset's content-type
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The number of times this asset was downloaded
		/// </summary>
		public int DownloadCount { get; set; }

		/// <summary>
		/// Identifies the URL where you can download the release asset via the browser.
		/// </summary>
		public string DownloadUrl { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Identifies the title of the release asset.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Release that the asset is associated with
		/// </summary>
		public Release Release { get; set; }

		/// <summary>
		/// The size (in bytes) of the asset
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The user that performed the upload
		/// </summary>
		public User UploadedBy { get; set; }

		/// <summary>
		/// Identifies the URL of the release asset.
		/// </summary>
		public string Url { get; set; }
	}
}
