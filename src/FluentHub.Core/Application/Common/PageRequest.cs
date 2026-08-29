// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core
{
	public sealed record PageRequest(
		int? First = null,
		string? After = null,
		int? Last = null,
		string? Before = null)
	{
		public static PageRequest Forward(int count, string? after = null)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
			return new(First: count, After: after);
		}

		public static PageRequest Backward(int count, string? before = null)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
			return new(Last: count, Before: before);
		}
	}
}
