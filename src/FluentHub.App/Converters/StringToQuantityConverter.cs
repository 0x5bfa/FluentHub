// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	public class StringToQuantityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var val = System.Convert.ToInt32(value);
			var param = parameter.ToString();

			return param.ToQuantity(val);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
			=> throw new NotImplementedException();
	}
}
