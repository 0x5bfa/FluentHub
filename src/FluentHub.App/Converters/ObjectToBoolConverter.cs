// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
	public partial class ObjectToBoolConverter : IValueConverter
	{
		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			if (parameter is string param && string.Compare(param, "invert", true) == 0)
			{
				// invert Convert
				var result = Convert(value, targetType, null, language);

				if (result is bool r)
					return !r;

				if (result is Visibility v)
					return v == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			}

			// Check the property type, sometimes is IsEnabled/x:Load (return a bool), sometimes is Visibility (return Microsoft.UI.Xaml.Visibility)
			// Using a converter, UIElement.Visibility disables the cast from bool to Microsoft.UI.Xaml.Visibility
			object trueValue;
			object falseValue;

			if (targetType == typeof(Visibility))
			{
				trueValue = Visibility.Visible;
				falseValue = Visibility.Collapsed;
			}
			else
			{
				trueValue = true;
				falseValue = false;
			}

			if (value is null)
				return falseValue;

			if (value is string s)
				return string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s) ? falseValue : trueValue;

			if (value is bool boolean)
			{
				return boolean ? trueValue : falseValue;
			}

			if (value.GetType().IsEnum)
			{
				return System.Convert.ToInt64(value) == 0 ? falseValue : trueValue;
			}

			if (value is IConvertible convertible)
			{
				try
				{
					return convertible.ToDecimal(null) == 0 ? falseValue : trueValue;
				}
				catch (InvalidCastException)
				{
					// Non-numeric convertible values are non-empty objects.
				}
			}

			return value == default ? falseValue : trueValue;
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language) => throw new NotImplementedException();
	}
}
