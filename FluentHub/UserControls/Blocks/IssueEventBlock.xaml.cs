using System;
using System.Collections.Generic;
using System.IO;
using FluentHub.ViewModels.UserControls.Blocks;
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
    public sealed partial class IssueEventBlock : UserControl
    {
        public static readonly DependencyProperty PropertyViewModelProperty =
       DependencyProperty.Register(nameof(PropertyViewModel), typeof(IssueEventBlockViewModel), typeof(IssueEventBlock), new PropertyMetadata(0));

        public IssueEventBlockViewModel PropertyViewModel
        {
            get => (IssueEventBlockViewModel)GetValue(PropertyViewModelProperty);
            set
            {
                SetValue(PropertyViewModelProperty, value);
                ViewModel = PropertyViewModel;
            }
        }

        public IssueEventBlock()
        {
            this.InitializeComponent();
        }
    }
}
