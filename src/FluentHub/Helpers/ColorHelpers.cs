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
            if (hexColorCode[0] == '#')
            {
                return new SolidColorBrush(
                    Color.FromArgb(0xFF
                    , Convert.ToByte(hexColorCode.Substring(1, 2), 16)
                    , Convert.ToByte(hexColorCode.Substring(3, 2), 16)
                    , Convert.ToByte(hexColorCode.Substring(5, 2), 16)));
            }
            else
            {
                return new SolidColorBrush(
                    Color.FromArgb(0xFF
                    , Convert.ToByte(hexColorCode.Substring(0, 2), 16)
                    , Convert.ToByte(hexColorCode.Substring(2, 2), 16)
                    , Convert.ToByte(hexColorCode.Substring(4, 2), 16)));
            }
        }
    }
}
