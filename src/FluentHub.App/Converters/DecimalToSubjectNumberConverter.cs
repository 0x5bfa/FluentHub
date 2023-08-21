// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	internal class DecimalToSubjectNumberConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return $"#{value}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
			=> throw new NotImplementedException();
	}
}
