using FluentHub.Uwp.ViewModels.UserControls.FeedBlocks;
using System;
using System.Collections.Generic;
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

namespace FluentHub.Uwp.UserControls.FeedBlocks
{
    public sealed partial class SingleReleaseBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(SingleReleaseBlockViewModel),
                typeof(SingleReleaseBlock),
                new PropertyMetadata(null));

        public SingleReleaseBlockViewModel ViewModel
        {
            get => (SingleReleaseBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public SingleReleaseBlock() => InitializeComponent();
    }
}
