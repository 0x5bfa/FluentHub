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
            set => SetValue(WidthAndHeightProperty, value);
        }

        public static readonly DependencyProperty AvatarUrlProperty =
            DependencyProperty.Register(
                nameof(AvatarUrl),
                typeof(string),
                typeof(AvatarControl),
                null);

        public string AvatarUrl
        {
            get => (string)GetValue(AvatarUrlProperty);
            set
            {
                SetValue(AvatarUrlProperty, value);
                BitMapImage.UriSource = new Uri(AvatarUrl);
            }
        }

        public static readonly DependencyProperty IsOrganizationProperty =
            DependencyProperty.Register(
                nameof(IsOrganization),
                typeof(bool),
                typeof(AvatarControl),
                null);

        public bool IsOrganization
        {
            get => (bool)GetValue(IsOrganizationProperty);
            set => SetValue(IsOrganizationProperty, value);
        }
        #endregion

        public AvatarControl()
        {
            this.InitializeComponent();
        }

        private void OnAvatarControlLoaded(object sender, RoutedEventArgs e)
        {
            if (WidthAndHeight == 0)
            {
                return;
            }

            AvatarImageBorder.Height = WidthAndHeight;
            AvatarImageBorder.Width = WidthAndHeight;

            if (IsOrganization)
            {
                AvatarImageBorder.CornerRadius = new CornerRadius(6);
            }
            else
            {
                AvatarImageBorder.CornerRadius = new CornerRadius(WidthAndHeight / 2);
            }
        }
    }
}
