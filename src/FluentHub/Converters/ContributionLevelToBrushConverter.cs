// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Models;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace FluentHub.Converters
{
	public sealed partial class ContributionLevelToBrushConverter : IValueConverter
	{
		public bool IsLightTheme { get; set; }

		public object Convert(object? value, Type targetType, object? parameter, string language)
		{
			var level = value is ContributionLevel contributionLevel
				? contributionLevel
				: ContributionLevel.None;

			var color = (IsLightTheme, level) switch
			{
				(true, ContributionLevel.FirstQuartile) => Color.FromArgb(255, 155, 233, 168),
				(true, ContributionLevel.SecondQuartile) => Color.FromArgb(255, 64, 196, 99),
				(true, ContributionLevel.ThirdQuartile) => Color.FromArgb(255, 48, 161, 78),
				(true, ContributionLevel.FourthQuartile) => Color.FromArgb(255, 33, 110, 57),
				(true, _) => Color.FromArgb(255, 235, 237, 240),
				(false, ContributionLevel.FirstQuartile) => Color.FromArgb(255, 14, 68, 41),
				(false, ContributionLevel.SecondQuartile) => Color.FromArgb(255, 0, 109, 50),
				(false, ContributionLevel.ThirdQuartile) => Color.FromArgb(255, 38, 166, 65),
				(false, ContributionLevel.FourthQuartile) => Color.FromArgb(255, 57, 211, 83),
				_ => Color.FromArgb(255, 22, 27, 34),
			};

			return new SolidColorBrush(color);
		}

		public object ConvertBack(object? value, Type targetType, object? parameter, string language)
			=> throw new NotImplementedException();
	}
}
