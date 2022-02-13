using FluentHub.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FluentHub.UserControls.Labels
{
    public sealed partial class LabelControl : UserControl
    {
        #region AccentColorProperty
        public static readonly DependencyProperty AccentColorProperty
        = DependencyProperty.Register(
              nameof(LabelControl),
              typeof(Brush),
              typeof(LabelControl),
              new PropertyMetadata(null)
            );

        public Brush AccentColor
        {
            get => (Brush)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }
        #endregion

        #region FontGlyphProperty
        public static readonly DependencyProperty FontGlyphProperty
            = DependencyProperty.Register(
                  nameof(FontGlyph),
                  typeof(string),
                  typeof(LabelControl),
                  new PropertyMetadata(null)
                );

        public string FontGlyph
        {
            get => (string)GetValue(FontGlyphProperty);
            set => SetValue(FontGlyphProperty, value);
        }
        #endregion

        #region LabelTextProperty
        public static readonly DependencyProperty LabelTextProperty
            = DependencyProperty.Register(
                  nameof(LabelText),
                  typeof(string),
                  typeof(LabelControl),
                  new PropertyMetadata(null)
                );

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        #endregion

        public LabelControl()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LabelBackground.Background = AccentColor;
            LabelBackground.Opacity = 0.18;
            LabelBorder.Opacity = 0.3;

            Color color = ((SolidColorBrush)AccentColor).Color;

            //float H = 0; float S = 0; float L = 0;
            //ColorHelpers.RgbToHsl(color.R, color.G, color.B, out H, out S, out L);

            //SetColorAdjustedBorderAndColor(color.R, color.G, color.B, H, S, L);

            LabelBorder.BorderBrush = AccentColor;
            LabelTextBlock.Foreground = AccentColor;

            LabelTextBlock.Text = LabelText;
        }

        private void SetColorAdjustedBorderAndColor(int R, int G, int B, float H, float S, float L)
        {
            // This method was created from github web-site style named flash.scss
            // but I could not update color blightness correctly. I don't know why.
            float lightnessThreshold = 0.6F;

            float perceivedLightness = ((R * 0.2126F) + (G * 0.7152F) + (B * 0.0722F)) / 255F;
            float lightnessSwitch = (perceivedLightness - lightnessThreshold) * -1000;
            lightnessSwitch = ((lightnessSwitch < 1) ? lightnessSwitch : 1);
            lightnessSwitch =( (lightnessSwitch < 0) ? 0 : lightnessSwitch);

            float lightenBy = ((lightnessThreshold - perceivedLightness) * 100) * lightnessSwitch;

            S *= 0.01F;
            L = (L + lightenBy) * 0.01F;

            ColorHelpers.HslToRgb(H, S, L, out R, out G, out B);

            var adjustedBrush = new SolidColorBrush(Color.FromArgb(0xFF, (byte)R, (byte)G, (byte)B));
            LabelBorder.BorderBrush = adjustedBrush;
            LabelTextBlock.Foreground = adjustedBrush;
        }
    }
}
