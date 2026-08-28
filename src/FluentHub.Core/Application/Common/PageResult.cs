// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core
{
	public sealed record PageResult<T>(IReadOnlyList<T> Items, PageInfo PageInfo);
}
