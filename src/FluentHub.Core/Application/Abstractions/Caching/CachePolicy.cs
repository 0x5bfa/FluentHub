// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Abstractions.Caching
{
	public readonly record struct CachePolicy
	{
		public CachePolicy(TimeSpan freshFor, TimeSpan retainFor)
		{
			if (freshFor < TimeSpan.Zero)
				throw new ArgumentOutOfRangeException(nameof(freshFor));
			if (retainFor < freshFor)
				throw new ArgumentOutOfRangeException(nameof(retainFor));

			FreshFor = freshFor;
			RetainFor = retainFor;
		}

		public TimeSpan FreshFor { get; }

		public TimeSpan RetainFor { get; }
	}

	public static class CachePolicies
	{
		public static CachePolicy Image { get; } = new(TimeSpan.FromDays(30), TimeSpan.FromDays(90));

		public static CachePolicy User { get; } = new(TimeSpan.FromMinutes(15), TimeSpan.FromDays(7));

		public static CachePolicy Repository { get; } = new(TimeSpan.FromMinutes(10), TimeSpan.FromDays(7));

		public static CachePolicy Organization { get; } = new(TimeSpan.FromMinutes(15), TimeSpan.FromDays(7));
	}
}
