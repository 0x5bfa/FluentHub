// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace FluentHub.Core.Caching
{
	public sealed class FileCacheService : ICacheService
	{
		private const int CacheFormatVersion = 1;

		private readonly ConcurrentDictionary<string, MemoryEntry> _memoryCache = new(StringComparer.Ordinal);
		private readonly ConcurrentDictionary<string, SemaphoreSlim> _entryLocks = new(StringComparer.Ordinal);
		private readonly SemaphoreSlim _diskGate = new(1, 1);
		private readonly SemaphoreSlim _trimGate = new(1, 1);
		private readonly string _rootPath;
		private readonly TimeProvider _timeProvider;
		private readonly long _maximumSizeBytes;
		private int _generation;

		public FileCacheService(
			string rootPath,
			long maximumSizeBytes = 320L * 1024 * 1024,
			TimeProvider? timeProvider = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
			if (maximumSizeBytes <= 0)
				throw new ArgumentOutOfRangeException(nameof(maximumSizeBytes));

			_rootPath = Path.GetFullPath(rootPath);
			if (string.Equals(_rootPath, Path.GetPathRoot(_rootPath), StringComparison.OrdinalIgnoreCase))
				throw new ArgumentException("The cache root cannot be a filesystem root.", nameof(rootPath));

			_maximumSizeBytes = maximumSizeBytes;
			_timeProvider = timeProvider ?? TimeProvider.System;
			Directory.CreateDirectory(_rootPath);
		}

		public Task<T> GetOrCreateAsync<T>(
			CacheKey key,
			CachePolicy policy,
			CacheSerializer<T> serializer,
			Func<CancellationToken, Task<T>> valueFactory,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(serializer);
			ArgumentNullException.ThrowIfNull(valueFactory);

			return GetOrCreateCoreAsync(
				key,
				policy,
				valueFactory,
				serializer.Serialize,
				serializer.Deserialize,
				cancellationToken);
		}

		public Task<byte[]> GetOrCreateBytesAsync(
			CacheKey key,
			CachePolicy policy,
			Func<CancellationToken, Task<byte[]>> valueFactory,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(valueFactory);

			return GetOrCreateCoreAsync(
				key,
				policy,
				valueFactory,
				static value => value,
				static bytes => bytes,
				cancellationToken);
		}

		public async Task RemoveAsync(CacheKey key, CancellationToken cancellationToken = default)
		{
			_memoryCache.TryRemove(key.Identity, out _);
			var paths = GetPaths(key);

			await _diskGate.WaitAsync(cancellationToken);
			try
			{
				DeleteIfExists(paths.DataPath);
				DeleteIfExists(paths.MetadataPath);
			}
			finally
			{
				_diskGate.Release();
			}
		}

		public async Task ClearAsync(CancellationToken cancellationToken = default)
		{
			Interlocked.Increment(ref _generation);

			await _diskGate.WaitAsync(cancellationToken);
			try
			{
				_memoryCache.Clear();

				if (Directory.Exists(_rootPath))
					Directory.Delete(_rootPath, recursive: true);

				Directory.CreateDirectory(_rootPath);
			}
			finally
			{
				_diskGate.Release();
			}
		}

		public async Task<long> GetSizeAsync(CancellationToken cancellationToken = default)
		{
			await _diskGate.WaitAsync(cancellationToken);
			try
			{
				if (!Directory.Exists(_rootPath))
					return 0;

				long total = 0;
				foreach (var filePath in Directory.EnumerateFiles(_rootPath, "*", SearchOption.AllDirectories))
				{
					cancellationToken.ThrowIfCancellationRequested();

					try
					{
						total += new FileInfo(filePath).Length;
					}
					catch (FileNotFoundException)
					{
					}
				}

				return total;
			}
			finally
			{
				_diskGate.Release();
			}
		}

		private async Task<T> GetOrCreateCoreAsync<T>(
			CacheKey key,
			CachePolicy policy,
			Func<CancellationToken, Task<T>> valueFactory,
			Func<T, byte[]> serialize,
			Func<byte[], T> deserialize,
			CancellationToken cancellationToken)
		{
			var cached = await TryReadAsync(key, deserialize, cancellationToken);
			if (cached.State == CacheEntryState.Fresh)
				return cached.Value!;

			if (cached.State == CacheEntryState.Stale)
			{
				_ = RefreshInBackgroundAsync(key, policy, valueFactory, serialize, deserialize);
				return cached.Value!;
			}

			return await RefreshAsync(key, policy, valueFactory, serialize, deserialize, cancellationToken);
		}

		private async Task RefreshInBackgroundAsync<T>(
			CacheKey key,
			CachePolicy policy,
			Func<CancellationToken, Task<T>> valueFactory,
			Func<T, byte[]> serialize,
			Func<byte[], T> deserialize)
		{
			try
			{
				await RefreshAsync(key, policy, valueFactory, serialize, deserialize, CancellationToken.None);
			}
			catch
			{
				// A stale value remains usable when a background refresh fails.
			}
		}

		private async Task<T> RefreshAsync<T>(
			CacheKey key,
			CachePolicy policy,
			Func<CancellationToken, Task<T>> valueFactory,
			Func<T, byte[]> serialize,
			Func<byte[], T> deserialize,
			CancellationToken cancellationToken)
		{
			var entryLock = _entryLocks.GetOrAdd(key.Identity, static _ => new SemaphoreSlim(1, 1));
			await entryLock.WaitAsync(cancellationToken);

			try
			{
				var cached = await TryReadAsync(key, deserialize, cancellationToken);
				if (cached.State == CacheEntryState.Fresh)
					return cached.Value!;

				var generation = Volatile.Read(ref _generation);
				var value = await valueFactory(cancellationToken);
				if (value is null)
					throw new InvalidOperationException("Cache factories cannot return null.");

				await StoreAsync(key, value, policy, serialize, generation, cancellationToken);

				return value;
			}
			finally
			{
				entryLock.Release();
			}
		}

		private async Task<CacheReadResult<T>> TryReadAsync<T>(
			CacheKey key,
			Func<byte[], T> deserialize,
			CancellationToken cancellationToken)
		{
			var now = _timeProvider.GetUtcNow();
			if (_memoryCache.TryGetValue(key.Identity, out var memoryEntry) && memoryEntry.Value is T memoryValue)
			{
				var state = GetState(memoryEntry.FreshUntilUtc, memoryEntry.RetainUntilUtc, now);
				if (state != CacheEntryState.Missing)
					return new(state, memoryValue);

				_memoryCache.TryRemove(key.Identity, out _);
			}

			var paths = GetPaths(key);
			await _diskGate.WaitAsync(cancellationToken);

			try
			{
				if (!File.Exists(paths.DataPath) || !File.Exists(paths.MetadataPath))
					return CacheReadResult<T>.Missing;

				try
				{
					var metadataText = await File.ReadAllTextAsync(paths.MetadataPath, cancellationToken);
					var metadata = ParseMetadata(metadataText);
					if (metadata.Version != CacheFormatVersion)
						throw new InvalidDataException("Unsupported cache metadata.");

					var state = GetState(metadata.FreshUntilUtc, metadata.RetainUntilUtc, now);
					if (state == CacheEntryState.Missing)
					{
						DeleteIfExists(paths.DataPath);
						DeleteIfExists(paths.MetadataPath);
						return CacheReadResult<T>.Missing;
					}

					var bytes = await File.ReadAllBytesAsync(paths.DataPath, cancellationToken);
					var value = deserialize(bytes);
					_memoryCache[key.Identity] = new(value!, metadata.FreshUntilUtc, metadata.RetainUntilUtc);

					File.SetLastAccessTimeUtc(paths.DataPath, now.UtcDateTime);
					File.SetLastAccessTimeUtc(paths.MetadataPath, now.UtcDateTime);

					return new(state, value);
				}
				catch (OperationCanceledException)
				{
					throw;
				}
				catch
				{
					DeleteIfExists(paths.DataPath);
					DeleteIfExists(paths.MetadataPath);
					return CacheReadResult<T>.Missing;
				}
			}
			finally
			{
				_diskGate.Release();
			}
		}

		private async Task StoreAsync<T>(
			CacheKey key,
			T value,
			CachePolicy policy,
			Func<T, byte[]> serialize,
			int generation,
			CancellationToken cancellationToken)
		{
			var now = _timeProvider.GetUtcNow();
			var metadata = new CacheMetadata
			{
				Version = CacheFormatVersion,
				FreshUntilUtc = now.Add(policy.FreshFor),
				RetainUntilUtc = now.Add(policy.RetainFor),
			};
			var bytes = serialize(value);
			var metadataBytes = Encoding.UTF8.GetBytes(FormatMetadata(metadata));
			var paths = GetPaths(key);

			await _diskGate.WaitAsync(cancellationToken);
			try
			{
				if (generation != Volatile.Read(ref _generation))
					return;

				Directory.CreateDirectory(paths.DirectoryPath);
				await WriteAtomicallyAsync(paths.DataPath, bytes, cancellationToken);
				await WriteAtomicallyAsync(paths.MetadataPath, metadataBytes, cancellationToken);
				_memoryCache[key.Identity] = new(value!, metadata.FreshUntilUtc, metadata.RetainUntilUtc);
			}
			finally
			{
				_diskGate.Release();
			}

			_ = TrimIfNeededAsync();
		}

		private async Task TrimIfNeededAsync()
		{
			if (!await _trimGate.WaitAsync(0))
				return;

			try
			{
				await _diskGate.WaitAsync();
				try
				{
					if (!Directory.Exists(_rootPath))
						return;

					var dataFiles = Directory
						.EnumerateFiles(_rootPath, "*.data", SearchOption.AllDirectories)
						.Select(static path => new FileInfo(path))
						.OrderBy(static file => file.LastAccessTimeUtc)
						.ToList();
					long size = dataFiles.Sum(static file => file.Length);
					if (size <= _maximumSizeBytes)
						return;

					var targetSize = (long)(_maximumSizeBytes * 0.9);
					foreach (var file in dataFiles)
					{
						var fileLength = file.Length;
						DeleteIfExists(file.FullName);
						DeleteIfExists(Path.ChangeExtension(file.FullName, ".meta"));
						size -= fileLength;

						if (size <= targetSize)
							break;
					}
				}
				finally
				{
					_diskGate.Release();
				}
			}
			catch
			{
				// Cache trimming must never prevent a successful read or write.
			}
			finally
			{
				_trimGate.Release();
			}
		}

		private CachePaths GetPaths(CacheKey key)
		{
			var partition = MakeSafePathSegment(key.Partition);
			var category = MakeSafePathSegment(key.Category);
			var directory = Path.Combine(_rootPath, partition, category);
			var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(key.Identity))).ToLowerInvariant();
			var shard = hash[..2];
			directory = Path.Combine(directory, shard);

			return new(
				directory,
				Path.Combine(directory, hash + ".data"),
				Path.Combine(directory, hash + ".meta"));
		}

		private static string MakeSafePathSegment(string value)
		{
			var builder = new StringBuilder(Math.Min(value.Length, 64));
			foreach (var character in value.Take(64))
				builder.Append(char.IsLetterOrDigit(character) || character is '-' or '_' ? character : '_');

			return builder.Length == 0 ? "default" : builder.ToString();
		}

		private static CacheEntryState GetState(
			DateTimeOffset freshUntilUtc,
			DateTimeOffset retainUntilUtc,
			DateTimeOffset now)
		{
			if (now <= freshUntilUtc)
				return CacheEntryState.Fresh;
			if (now <= retainUntilUtc)
				return CacheEntryState.Stale;

			return CacheEntryState.Missing;
		}

		private static string FormatMetadata(CacheMetadata metadata)
			=> string.Join(
				'\n',
				metadata.Version.ToString(CultureInfo.InvariantCulture),
				metadata.FreshUntilUtc.UtcTicks.ToString(CultureInfo.InvariantCulture),
				metadata.RetainUntilUtc.UtcTicks.ToString(CultureInfo.InvariantCulture));

		private static CacheMetadata ParseMetadata(string value)
		{
			var parts = value.Split('\n');
			if (parts.Length != 3 ||
				!int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out var version) ||
				!long.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var freshUntilTicks) ||
				!long.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var retainUntilTicks))
			{
				throw new InvalidDataException("Invalid cache metadata.");
			}

			return new()
			{
				Version = version,
				FreshUntilUtc = new DateTimeOffset(freshUntilTicks, TimeSpan.Zero),
				RetainUntilUtc = new DateTimeOffset(retainUntilTicks, TimeSpan.Zero),
			};
		}

		private static async Task WriteAtomicallyAsync(
			string destinationPath,
			byte[] content,
			CancellationToken cancellationToken)
		{
			var temporaryPath = destinationPath + "." + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + ".tmp";
			try
			{
				await File.WriteAllBytesAsync(temporaryPath, content, cancellationToken);
				File.Move(temporaryPath, destinationPath, overwrite: true);
			}
			finally
			{
				DeleteIfExists(temporaryPath);
			}
		}

		private static void DeleteIfExists(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch (DirectoryNotFoundException)
			{
			}
		}

		private sealed record MemoryEntry(
			object Value,
			DateTimeOffset FreshUntilUtc,
			DateTimeOffset RetainUntilUtc);

		private sealed class CacheMetadata
		{
			public int Version { get; set; }

			public DateTimeOffset FreshUntilUtc { get; set; }

			public DateTimeOffset RetainUntilUtc { get; set; }
		}

		private readonly record struct CachePaths(
			string DirectoryPath,
			string DataPath,
			string MetadataPath);

		private readonly record struct CacheReadResult<T>(CacheEntryState State, T? Value)
		{
			public static CacheReadResult<T> Missing { get; } = new(CacheEntryState.Missing, default);
		}

		private enum CacheEntryState
		{
			Missing,
			Fresh,
			Stale,
		}
	}
}
