// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can be deleted.
	/// </summary>
	public interface IDeletable
	{
		/// <summary>
		/// Check if the current viewer can delete this object.
		/// </summary>
		bool ViewerCanDelete { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Deletable : IDeletable
	{
		public bool ViewerCanDelete { get; set; }
	}
}

