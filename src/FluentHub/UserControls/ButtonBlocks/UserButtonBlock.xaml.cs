using FluentHub.OctokitEx.Models;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Octokit;
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
    public sealed partial class UserButtonBlock : UserControl
    {
        #region properties
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(UserBlockItem), typeof(UserButtonBlockViewModel), typeof(UserButtonBlock), new PropertyMetadata(null));

        public UserButtonBlockViewModel ViewModel
        {
            get => (UserButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }
        #endregion

        public UserButtonBlock()
        {
            this.InitializeComponent();
        }

        private void UserButtonBlockButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.MainFrame.Navigate(typeof(Views.Users.ProfilePage), ViewModel.User.Login);
        }
    }
}
