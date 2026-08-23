// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace FluentHub.Core.Caching
{
	public sealed class CacheSerializer<T>
	{
		public CacheSerializer(Func<T, byte[]> serialize, Func<byte[], T> deserialize)
		{
			Serialize = serialize ?? throw new ArgumentNullException(nameof(serialize));
			Deserialize = deserialize ?? throw new ArgumentNullException(nameof(deserialize));
		}

		internal Func<T, byte[]> Serialize { get; }

		internal Func<byte[], T> Deserialize { get; }

		public static CacheSerializer<T> FromJsonTypeInfo(JsonTypeInfo<T> typeInfo)
		{
			ArgumentNullException.ThrowIfNull(typeInfo);

			return new(
				value => System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value, typeInfo),
				bytes => System.Text.Json.JsonSerializer.Deserialize(bytes, typeInfo)
					?? throw new System.Text.Json.JsonException($"Cached {typeof(T).Name} was null."));
		}
	}

	public static class CacheSerializers
	{
		public static CacheSerializer<string> String { get; } = new(
			static value => Encoding.UTF8.GetBytes(value),
			static bytes => Encoding.UTF8.GetString(bytes));
	}
}
