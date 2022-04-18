using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.Views.Repositories.Codes;
using FluentHub.Views.Repositories.Issues;
using FluentHub.Views.Repositories.PullRequests;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Extensions.DependencyInjection;
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
