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


namespace FluentHub.UserControls.Label
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
            LabelBorder.BorderBrush = AccentColor;
            LabelBackground.Background = AccentColor;
            LabelTextBlock.Foreground = AccentColor;

            LabelTextBlock.Text = LabelText;
        }
    }
}
