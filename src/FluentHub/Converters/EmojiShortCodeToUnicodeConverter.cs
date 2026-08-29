// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.Converters
{
	public partial class EmojiShortCodeToUnicodeConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			var shortCode = value?.ToString();

			if (string.IsNullOrEmpty(shortCode))
				return string.Empty;

			return Core.Extensions.Emoji.EmojiMapping.GetUnicode(shortCode);
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
