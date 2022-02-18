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
        public static void RgbToHsl(int R, int G, int B, out float H, out float S, out float L)
        {
            float dr = (float)R / (float)255;
            float dg = (float)G / (float)255;
            float db = (float)B / (float)255;

            float maxv = Math.Max(Math.Max(dr, dg), db);
            float minv = Math.Min(Math.Min(dr, dg), db);

            float d_hue = 0;
            if (maxv - minv == 0)
            {
                H = 0; S = 0; L = 0;
                return;
            }
            else
            {
                if (maxv == dr)
                {
                    d_hue = 60 * ((dg - db) / (maxv - minv)) + 0;
                }
                else if (maxv == dg)
                {
                    d_hue = 60 * ((db - dr) / (maxv - minv)) + 120;
                }
                //else if (maxv == db) {
                else
                {
                    d_hue = 60 * ((dr - dg) / (maxv - minv)) + 240;
                }
                if (d_hue < 0) d_hue += 360;
            }

            float d_lightness = (maxv + minv) / 2.0f;
            float d_lightness_per = d_lightness * 100;

            float d_saturation;
            if (d_lightness < 0.5f)
            {
                d_saturation = (maxv - minv) / (maxv + minv);
            }
            else
            {
                d_saturation = (maxv - minv) / (2.0f - maxv - minv);
            }

            H = d_hue;
            S = d_saturation * 100;
            L = d_lightness * 100;
        }

        public static void HslToRgb(float H, float L, float S, out int R, out int G, out int B)
        {
            double d_hue = H;
            double d_lightness = L;
            double d_saturation = S;

            if (d_hue < 0)
            {
                d_hue = d_hue + 360;
            }
            else if (d_hue > 360)
            {
                d_hue = d_hue - 360;
            }

            double Hi = d_hue % 360;

            if (d_saturation == 0)
            {
                R = (int)(d_lightness * 255);
                G = (int)(d_lightness * 255);
                B = (int)(d_lightness * 255);
            }
            else
            {
                double d_c = (1.0f - Math.Abs(2.0f * d_lightness - 1)) * d_saturation;
                double H_d = d_hue / 60.0f;
                double d_x = d_c * (1 - Math.Abs((H_d % 2) - 1));

                double R1, G1, B1;
                if (0 <= H_d && H_d < 1)
                {
                    R1 = d_c;
                    G1 = d_x;
                    B1 = 0;
                }
                else if (1 <= H_d && H_d < 2)
                {
                    R1 = d_x;
                    G1 = d_c;
                    B1 = 0;
                }
                else if (2 <= H_d && H_d < 3)
                {
                    R1 = 0;
                    G1 = d_c;
                    B1 = d_x;
                }
                else if (3 <= H_d && H_d < 4)
                {
                    R1 = 0;
                    G1 = d_x;
                    B1 = d_c;
                }
                else if (4 <= H_d && H_d < 5)
                {
                    R1 = d_x;
                    G1 = 0;
                    B1 = d_c;
                }
                else if (5 <= H_d && H_d < 6)
                {
                    R1 = d_c;
                    G1 = 0;
                    B1 = d_x;
                }
                else
                {
                    R1 = 0;
                    G1 = 0;
                    B1 = 0;
                }


                double m = d_lightness - d_c * 0.5f;
                R = (int)((R1 + m) * 255);
                G = (int)((G1 + m) * 255);
                B = (int)((B1 + m) * 255);

            }
        }

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
