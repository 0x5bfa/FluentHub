// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public class CustomRepositoryResponseForCodePage
	{
		public Repository? Repository { get; set; }

		public int TagsTotalCount { get; set; }

		public int BranchesTotalCount { get; set; }
	}
}
