// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core
{
	public sealed record PageResult<T>(IReadOnlyList<T> Items, PageInfo PageInfo);
}
