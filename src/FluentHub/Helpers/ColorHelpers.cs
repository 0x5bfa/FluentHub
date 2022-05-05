using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace FluentHub.Helpers
{
    public class ColorHelpers
    {
        /// <summary>
        /// Converts from hexadecimal RGB color code to SolidColorBrush.
        /// </summary>
        /// <param name="hexColorCode">Hexadecimal color code where # may be at the beginning of the string</param>
        /// <returns></returns>
        public static SolidColorBrush HexCodeToSolidColorBrush(string hexColorCode)
        {
            if (string.IsNullOrEmpty(hexColorCode)) return null;

            string normalizedColorCode = "#00000000";

            // If there's no alpha code, add them
            if (hexColorCode.Length == 6 || hexColorCode.Length == 7)
            {
                if (hexColorCode[0] == '#')
                {
                    normalizedColorCode = $"#FF{hexColorCode.Substring(1, hexColorCode.Length - 1)}";
                }
                else
                {
                    normalizedColorCode = $"#FF{hexColorCode}";
                }
            }
            else
            {
                normalizedColorCode = hexColorCode;
            }

            return new SolidColorBrush(
                Color.FromArgb(
                Convert.ToByte(normalizedColorCode.Substring(1, 2), 16),
                Convert.ToByte(normalizedColorCode.Substring(3, 2), 16),
                Convert.ToByte(normalizedColorCode.Substring(5, 2), 16),
                Convert.ToByte(normalizedColorCode.Substring(7, 2), 16)));
        }
    }
}
