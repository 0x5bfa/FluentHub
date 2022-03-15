using FluentHub.OctokitEx.Models;
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
    public sealed partial class OrgButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(OrganizationOverviewItem),
                  typeof(IssueButtonBlockViewModel),
                  typeof(OrgButtonBlock),
                  new PropertyMetadata(null)
                );

        public OrgButtonBlockViewModel ViewModel
        {
            get => (OrgButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }
        #endregion

        public OrgButtonBlock()
        {
            this.InitializeComponent();
        }

        private void OrganizationOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.MainFrame.Navigate(typeof(Views.Organizations.ProfilePage), ViewModel.OrgItem.Login);
        }
    }
}
