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
        #region propdp
        public static readonly DependencyProperty WidthAndHeightProperty =
            DependencyProperty.Register(
                nameof(WidthAndHeight),
                typeof(int),
                typeof(AvatarControl),
                new PropertyMetadata(0));

        public int WidthAndHeight
        {
            get => (int)GetValue(WidthAndHeightProperty);
            set
            {
                SetValue(WidthAndHeightProperty, value);
                AvatarImageBorder.Height = WidthAndHeight;
                AvatarImageBorder.Width = WidthAndHeight;
                AvatarImageBorder.CornerRadius = new CornerRadius(WidthAndHeight / 2);
            }
        }

        public static readonly DependencyProperty AvatarUrlProperty =
            DependencyProperty.Register(
                nameof(AvatarUrl),
                typeof(string),
                typeof(AvatarControl),
                new PropertyMetadata(null));

        public string AvatarUrl
        {
            get => (string)GetValue(AvatarUrlProperty);
            set
            {
                SetValue(AvatarUrlProperty, value);
                if(!string.IsNullOrEmpty(AvatarUrl))
                    BitMapImage.UriSource = new Uri(AvatarUrl);
            }
        }

        public static readonly DependencyProperty IsOrganizationProperty =
            DependencyProperty.Register(
                nameof(IsOrganization),
                typeof(bool),
                typeof(AvatarControl),
                new PropertyMetadata(false));

        public bool IsOrganization
        {
            get => (bool)GetValue(IsOrganizationProperty);
            set
            {
                SetValue(IsOrganizationProperty, value);
                if (IsOrganization) AvatarImageBorder.CornerRadius = new CornerRadius(6);
            }
        }
        #endregion

        public AvatarControl() => InitializeComponent();

        private void OnAvatarControlLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
