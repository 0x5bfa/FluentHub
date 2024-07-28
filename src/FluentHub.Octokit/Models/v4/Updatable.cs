// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can be updated.
	/// </summary>
	public interface IUpdatable
	{
		/// <summary>
		/// Check if the current viewer can update this object.
		/// </summary>
		bool ViewerCanUpdate { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Updatable : IUpdatable
	{
		public bool ViewerCanUpdate { get; set; }
	}
}

