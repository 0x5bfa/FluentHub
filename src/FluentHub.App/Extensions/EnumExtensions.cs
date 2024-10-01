using System.Reflection;

namespace FluentHub.App.Extensions
{
	public static class EnumExtensions
	{
		public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			var index = 0;

			foreach (var item in source)
			{
				if (predicate(item)) return index;

				index++;
			}

			return -1;
		}

		public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
		{
			if (!typeof(TEnum).GetTypeInfo().IsEnum)
			{
				throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
			}
			return (TEnum)Enum.Parse(typeof(TEnum), text);
		}
	}
}
