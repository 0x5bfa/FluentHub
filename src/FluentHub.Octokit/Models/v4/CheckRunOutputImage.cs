// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Images attached to the check run output displayed in the GitHub pull request UI.
	/// </summary>
	public class CheckRunOutputImage
	{
		/// <summary>
		/// The alternative text for the image.
		/// </summary>
		public string Alt { get; set; }

		/// <summary>
		/// The full URL of the image.
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// A short image description.
		/// </summary>
		public string Caption { get; set; }
	}
}
