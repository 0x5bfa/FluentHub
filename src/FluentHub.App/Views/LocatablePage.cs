// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Services.Navigation;
using FluentHub.App.ViewModels;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views
{
    public class LocatablePage : Page
    {
        public INavigationService NavigationService { get; }

        public ILogger Logger { get; }

        public LocatablePage()
        {
            var provider = App.Current.Services;
            NavigationService = provider.GetRequiredService<INavigationService>();
            Logger = provider.GetRequiredService<ILogger>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check if Navigation Bar exists or not

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
