using FluentHub.ViewModels.UserControls.ButtonBlocks;
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

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class NotificationButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(global::Octokit.Notification),
                  typeof(NotificationButtonBlockViewModel),
                  typeof(NotificationButtonBlock),
                  new PropertyMetadata(null)
                );

        public NotificationButtonBlockViewModel ViewModel
        {
            get => (NotificationButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }
        #endregion

        public NotificationButtonBlock()
        {
            this.InitializeComponent();
        }

        private async void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
