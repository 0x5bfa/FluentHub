// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Caching
{
	public readonly record struct CacheKey
	{
		public CacheKey(string partition, string category, string value)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(partition);
			ArgumentException.ThrowIfNullOrWhiteSpace(category);
			ArgumentException.ThrowIfNullOrWhiteSpace(value);

			Partition = partition;
			Category = category;
			Value = value;
		}

		public string Partition { get; }

		public string Category { get; }

		public string Value { get; }

		internal string Identity
			=> $"{Partition}\n{Category}\n{Value}";

		public static CacheKey Shared(string category, string value)
			=> new("shared", category, value);

		public static CacheKey ForAccount(string partition, string category, string value)
			=> new(partition, category, value);
	}
}
