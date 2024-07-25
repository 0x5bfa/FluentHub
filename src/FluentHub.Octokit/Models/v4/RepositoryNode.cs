// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents a object that belongs to a repository.
	/// </summary>
	public interface IRepositoryNode
	{
		/// <summary>
		/// The repository associated with this node.
		/// </summary>
		Repository Repository { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class RepositoryNode : IRepositoryNode
	{
		public Repository Repository { get; set; }
	}
}

