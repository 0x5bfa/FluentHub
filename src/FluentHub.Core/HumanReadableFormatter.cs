using System;
using System.Globalization;

namespace FluentHub.Core
{
	public static class HumanReadableFormatter
	{
		private static readonly string[] ByteUnits = ["B", "KB", "MB", "GB", "TB", "PB"];
		private static readonly string[] MetricPrefixes = ["", "kilo", "mega", "giga", "tera", "peta", "exa"];

		public static string FormatRelativeTime(DateTime value, DateTime referenceTime)
			=> FormatRelativeTime(value - referenceTime);

		public static string FormatRelativeTime(DateTimeOffset value, DateTimeOffset referenceTime)
			=> FormatRelativeTime(value - referenceTime);

		public static string FormatDuration(TimeSpan duration)
		{
			var elapsed = duration.Duration();

			if (elapsed.TotalDays >= 7)
				return FormatUnit((long)(elapsed.TotalDays / 7), "week");

			if (elapsed.TotalDays >= 1)
				return FormatUnit((long)elapsed.TotalDays, "day");

			if (elapsed.TotalHours >= 1)
				return FormatUnit((long)elapsed.TotalHours, "hour");

			if (elapsed.TotalMinutes >= 1)
				return FormatUnit((long)elapsed.TotalMinutes, "minute");

			if (elapsed.TotalSeconds >= 1)
				return FormatUnit((long)elapsed.TotalSeconds, "second");

			return FormatUnit((long)elapsed.TotalMilliseconds, "millisecond");
		}

		public static string FormatMetric(long value, IFormatProvider? formatProvider = null)
		{
			var provider = formatProvider ?? CultureInfo.CurrentCulture;
			var scaledValue = (double)value;
			var prefixIndex = 0;

			while (Math.Abs(scaledValue) >= 1000 && prefixIndex < MetricPrefixes.Length - 1)
			{
				scaledValue /= 1000;
				prefixIndex++;
			}

			return scaledValue.ToString(prefixIndex == 0 ? "0" : "0.#", provider) + MetricPrefixes[prefixIndex];
		}

		public static string FormatQuantity(string noun, long quantity, IFormatProvider? formatProvider = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(noun);

			var word = quantity == 1 ? noun : Pluralize(noun);
			return $"{quantity.ToString(formatProvider ?? CultureInfo.CurrentCulture)} {word}";
		}

		public static string FormatFileSize(long bytes, IFormatProvider? formatProvider = null)
		{
			var provider = formatProvider ?? CultureInfo.CurrentCulture;
			var size = Math.Abs((double)bytes);
			var unitIndex = 0;

			while (size >= 1024 && unitIndex < ByteUnits.Length - 1)
			{
				size /= 1024;
				unitIndex++;
			}

			if (bytes < 0)
				size = -size;

			return $"{size.ToString("0.##", provider)} {ByteUnits[unitIndex]}";
		}

		private static string FormatRelativeTime(TimeSpan difference)
		{
			if (Math.Abs(difference.TotalSeconds) < 1)
				return "now";

			var elapsed = difference.Duration();
			(long Value, string Unit) result = elapsed.TotalSeconds switch
			{
				< 60 => (RoundToPositiveInteger(elapsed.TotalSeconds), "second"),
				< 90 => (1, "minute"),
				_ when elapsed.TotalMinutes < 45 => (RoundToPositiveInteger(elapsed.TotalMinutes), "minute"),
				_ when elapsed.TotalMinutes < 90 => (1, "hour"),
				_ when elapsed.TotalHours < 24 => (RoundToPositiveInteger(elapsed.TotalHours), "hour"),
				_ when elapsed.TotalHours < 48 => (1, "day"),
				_ when elapsed.TotalDays < 28 => (RoundToPositiveInteger(elapsed.TotalDays), "day"),
				_ when elapsed.TotalDays < 30 => (1, "month"),
				_ when elapsed.TotalDays < 345 => (RoundToPositiveInteger(elapsed.TotalDays / 30), "month"),
				_ when elapsed.TotalDays < 545 => (1, "year"),
				_ => (RoundToPositiveInteger(elapsed.TotalDays / 365), "year")
			};

			var suffix = difference < TimeSpan.Zero ? "ago" : "from now";
			return $"{FormatUnit(result.Value, result.Unit)} {suffix}";
		}

		private static long RoundToPositiveInteger(double value)
			=> Math.Max(1, (long)Math.Round(value, MidpointRounding.AwayFromZero));

		private static string Pluralize(string noun)
		{
			if (noun.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
				noun.EndsWith("sh", StringComparison.OrdinalIgnoreCase) ||
				noun.EndsWith('s') ||
				noun.EndsWith('x') ||
				noun.EndsWith('z'))
			{
				return noun + "es";
			}

			if (noun.Length > 1 && noun.EndsWith('y') && !"aeiou".Contains(char.ToLowerInvariant(noun[^2])))
				return noun[..^1] + "ies";

			return noun + "s";
		}

		private static string FormatUnit(long value, string unit)
			=> $"{value.ToString(CultureInfo.CurrentCulture)} {unit}{(value == 1 ? string.Empty : "s")}";
	}

	public static class RelativeTimeExtensions
	{
		public static string ToRelativeTime(this DateTime value)
			=> HumanReadableFormatter.FormatRelativeTime(value, DateTime.Now);

		public static string ToRelativeTime(this DateTime? value)
			=> value is null ? "never" : value.Value.ToRelativeTime();

		public static string ToRelativeTime(this DateTimeOffset value)
			=> HumanReadableFormatter.FormatRelativeTime(value, DateTimeOffset.Now);

		public static string ToRelativeTime(this DateTimeOffset? value)
			=> value is null ? "never" : value.Value.ToRelativeTime();
	}
}
