using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	public class ObjectToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			// TODO: Support format strings with 'parameter'

			return value?.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
	}
}
