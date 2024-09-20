// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class PinnableRepository
	{
		public bool IsPinned { get; set; }

		public Repository PinnableItem { get; set; }
	}
}
