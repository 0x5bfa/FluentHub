using FluentHub.OctokitEx.Models;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Views.Repositories;
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
    public sealed partial class IssueButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(IssueOverviewItem),
                  typeof(IssueButtonBlockViewModel),
                  typeof(IssueButtonBlock),
                  new PropertyMetadata(null)
                );

        public IssueButtonBlockViewModel ViewModel
        {
            get => (IssueButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
                ViewModel.SetStateContents();
            }
        }
        #endregion

        public IssueButtonBlock()
        {
            this.InitializeComponent();
        }

        private async void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(ViewModel.IssueItem.Owner, ViewModel.IssueItem.Name);

            string param = repo.Id + "/" + ViewModel.IssueItem.Number;
            App.MainViewModel.RepoMainFrame.Navigate(typeof(IssuePage), param);
        }
    }
}
