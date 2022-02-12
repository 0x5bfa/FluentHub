using FluentHub.ViewModels.UserControls.Repository;
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

namespace FluentHub.UserControls.Repository
{
    public sealed partial class LatestCommitBlock : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty ViewModelProperty
        = DependencyProperty.Register(
              nameof(ViewModel),
              typeof(LatestCommitBlockViewModel),
              typeof(LatestCommitBlock),
              new PropertyMetadata(null)
            );

        public LatestCommitBlockViewModel ViewModel
        {
            get => (LatestCommitBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
                ViewModel.SetContents();
            }
        }
        #endregion

        public LatestCommitBlock()
        {
            this.InitializeComponent();
        }
    }
}
