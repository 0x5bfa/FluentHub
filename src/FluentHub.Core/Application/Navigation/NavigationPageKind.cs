// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Navigation
{
	public enum NavigationPageKind
	{
		/// <summary>
		/// Display no NavigationBar items.
		/// </summary>
		None,

		/// <summary>
		/// Display NavigationBar items for an organization.
		/// </summary>
		Organization,

		/// <summary>
		/// Display NavigationBar items for a repository.
		/// </summary>
		Repository,

		/// <summary>
		/// Display NavigationBar items for an user.
		/// </summary>
		User,
	}
}
