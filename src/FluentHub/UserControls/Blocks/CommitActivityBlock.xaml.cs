using FluentHub.ViewModels.UserControls.Blocks;
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
    public sealed partial class CommitActivityBlock : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(nameof(ViewModel), typeof(CommitActivityBlockViewModel), typeof(CommitActivityBlock), new PropertyMetadata(0));

        public CommitActivityBlockViewModel ViewModel
        {
            get => (CommitActivityBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                this.DataContext = ViewModel;
            }
        }

        public CommitActivityBlock()
        {
            this.InitializeComponent();
        }
    }
}
