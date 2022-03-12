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
    public sealed partial class PullButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(PullOverviewItem),
                  typeof(PullButtonBlockViewModel),
                  typeof(PullButtonBlock),
                  new PropertyMetadata(null)
                );

        public PullButtonBlockViewModel ViewModel
        {
            get => (PullButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }
        #endregion

        public PullButtonBlock()
        {
            this.InitializeComponent();
        }

        private void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
