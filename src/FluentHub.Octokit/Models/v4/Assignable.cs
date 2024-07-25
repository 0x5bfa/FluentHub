// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// An object that can have users assigned to it.
	/// </summary>
	public interface IAssignable
	{
		/// <summary>
		/// A list of Users assigned to this object.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		UserConnection Assignees { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Assignable : IAssignable
	{
		public UserConnection Assignees { get; set; }
	}
}

