using FluentHub.Models.Items;
using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Repositories.Codes;
using FluentHub.ViewModels.Repositories.Codes.Layouts;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Repositories.Codes.Layouts
{
    public sealed partial class TreeLayoutView : Page
    {
        public TreeLayoutView()
        {
            this.InitializeComponent();
        }
    }
}
