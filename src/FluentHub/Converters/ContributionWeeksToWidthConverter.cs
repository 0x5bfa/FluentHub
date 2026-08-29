// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Data;

namespace FluentHub.Converters
{
	public sealed partial class ContributionWeeksToWidthConverter : IValueConverter
	{
		private const double WeekWidth = 18;

		public object Convert(object? value, Type targetType, object? parameter, string language)
			=> value is int weeks ? Math.Max(0, weeks) * WeekWidth : 0d;

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
