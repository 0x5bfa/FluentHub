// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Caching
{
	public interface ICacheService
	{
		Task<T> GetOrCreateAsync<T>(
			CacheKey key,
			CachePolicy policy,
			CacheSerializer<T> serializer,
			Func<CancellationToken, Task<T>> valueFactory,
			CancellationToken cancellationToken = default);

		Task<byte[]> GetOrCreateBytesAsync(
			CacheKey key,
			CachePolicy policy,
			Func<CancellationToken, Task<byte[]>> valueFactory,
			CancellationToken cancellationToken = default);

		Task RemoveAsync(CacheKey key, CancellationToken cancellationToken = default);

		Task ClearAsync(CancellationToken cancellationToken = default);

		Task<long> GetSizeAsync(CancellationToken cancellationToken = default);
	}
}
