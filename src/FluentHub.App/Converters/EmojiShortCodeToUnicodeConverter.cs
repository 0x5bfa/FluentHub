using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	public class EmojiShortCodeToUnicodeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var shortCode = value?.ToString();

			if (string.IsNullOrEmpty(shortCode))
				return null;

			return Core.Extensions.Emoji.EmojiMapping.GetUnicode(shortCode);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
			=> throw new NotImplementedException();
	}
}
