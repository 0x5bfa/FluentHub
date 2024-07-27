// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// An object that can be locked.
	/// </summary>
	public interface ILockable
	{
		/// <summary>
		/// Reason that the conversation was locked.
		/// </summary>
		LockReason? ActiveLockReason { get; set; }

		/// <summary>
		/// `true` if the object is locked
		/// </summary>
		bool Locked { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Lockable : ILockable
	{
		public LockReason? ActiveLockReason { get; set; }

		public bool Locked { get; set; }
	}
}

