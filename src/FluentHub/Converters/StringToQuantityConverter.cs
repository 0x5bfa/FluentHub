// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.Converters
{
	public partial class StringToQuantityConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			var val = System.Convert.ToInt32(value);
			var param = parameter?.ToString() ?? string.Empty;

			return HumanReadableFormatter.FormatQuantity(param, val);
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
