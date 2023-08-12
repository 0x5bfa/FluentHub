// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// An object with an ID.
	/// </summary>
	public interface INode
	{
		/// <summary>
		/// ID of the object.
		/// </summary>
		ID Id { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Node : INode
	{
		public ID Id { get; set; }
	}
}

