using FluentHub.ViewModels.Repositories;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class FileNavigationBlock : UserControl
    {
        public static readonly DependencyProperty CommonRepoViewModelProperty =
            DependencyProperty.Register(
                nameof(CommonRepoViewModel),
                typeof(CommonRepoViewModel),
                typeof(FileNavigationBlock),
                new PropertyMetadata(0));

        public CommonRepoViewModel CommonRepoViewModel
        {
            get { return (CommonRepoViewModel)GetValue(CommonRepoViewModelProperty); }
            set
            {
                SetValue(CommonRepoViewModelProperty, value);
                ViewModel.CommonRepoViewModel = CommonRepoViewModel;
            }
        }

        public FileNavigationBlock()
        {
            this.InitializeComponent();
        }
    }
}
