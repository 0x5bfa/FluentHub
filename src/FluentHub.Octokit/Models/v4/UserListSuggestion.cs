// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a suggested user list.
	/// </summary>
	public class UserListSuggestion
	{
		/// <summary>
		/// The ID of the suggested user list
		/// </summary>
		public ID? Id { get; set; }

		/// <summary>
		/// The name of the suggested user list
		/// </summary>
		public string Name { get; set; }
	}
}
