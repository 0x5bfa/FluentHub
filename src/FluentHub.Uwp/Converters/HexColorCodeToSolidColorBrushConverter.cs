using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.Converters
{
    public class HexColorCodeToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var hexCode = value as string;

            string hexCodeWithAlpha;

            if (hexCode.Length == 6 || hexCode.Length == 7)
            {
                if (hexCode.ElementAt(0) == '#')
                {
                    hexCodeWithAlpha = $"#FF{hexCode.Substring(1, hexCode.Length - 1)}";
                }
                else
                {
                    hexCodeWithAlpha = $"#FF{hexCode}";
                }
            }
            else
            {
                hexCodeWithAlpha = hexCode;
            }

            return new SolidColorBrush(
                Color.FromArgb(
                System.Convert.ToByte(hexCodeWithAlpha.Substring(1, 2), 16),
                System.Convert.ToByte(hexCodeWithAlpha.Substring(3, 2), 16),
                System.Convert.ToByte(hexCodeWithAlpha.Substring(5, 2), 16),
                System.Convert.ToByte(hexCodeWithAlpha.Substring(7, 2), 16)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
