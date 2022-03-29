using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls
{
    public sealed partial class AvatarControl : UserControl
    {
        public static readonly DependencyProperty WidthAndHeightProperty =
            DependencyProperty.Register(
                nameof(WidthAndHeight),
                typeof(int),
                typeof(AvatarControl),
                new PropertyMetadata(0));

        public int WidthAndHeight
        {
            get { return (int)GetValue(WidthAndHeightProperty); }
            set { SetValue(WidthAndHeightProperty, value); }
        }

        public static readonly DependencyProperty AvatarUrlProperty =
            DependencyProperty.Register(
                nameof(AvatarUrl),
                typeof(string),
                typeof(AvatarControl),
                new PropertyMetadata(0));

        public string AvatarUrl
        {
            get { return (string)GetValue(AvatarUrlProperty); }
            set { SetValue(AvatarUrlProperty, value); }
        }

        public AvatarControl()
        {
            this.InitializeComponent();
        }

        private void OnAvatarControlLoaded(object sender, RoutedEventArgs e)
        {
            if (AvatarUrl == null || WidthAndHeight <= 0)
            {
                return;
            }

            AvatarImageBorder.Height = WidthAndHeight;
            AvatarImageBorder.Width = WidthAndHeight;
            AvatarImageBorder.CornerRadius = new CornerRadius(WidthAndHeight / 2);

            BitMapImage.UriSource = new Uri(AvatarUrl);
        }
    }
}
