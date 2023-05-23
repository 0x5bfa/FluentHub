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
using FluentHub.Core.Data.Enums;
using FluentHub.App.Data.Factories;
using FluentHub.App.Data.Items;

namespace FluentHub.App.Views
{
    public abstract class LocatablePage : Page
    {
        private INavigationService _navigationService;

        private ILogger _logger;

        private NavigationBarPageKind _currentPageKind;

        public LocatablePage(NavigationBarPageKind pageKind)
        {
            var provider = App.Current.Services;
            _navigationService = provider.GetRequiredService<INavigationService>();
            _logger = provider.GetRequiredService<ILogger>();
            _currentPageKind = pageKind;

            CheckIfNavigationBarShouldBeChanged();
        }

        public void CheckIfNavigationBarShouldBeChanged()
        {
            // Check if Navigation Bar exists or not
            if (_navigationService.PageKind != _currentPageKind)
            {
                // Generate new NavigationBar items from the factory
                _navigationService.NavigationBarItems ??= new();
                _navigationService.NavigationBarItems.Clear();

                var items = _currentPageKind switch
                {
                    NavigationBarPageKind.Organization => NavigationBarFactory.GetUserNavigationBarItems(),
                    NavigationBarPageKind.Repository => NavigationBarFactory.GetUserNavigationBarItems(),
                    NavigationBarPageKind.User => NavigationBarFactory.GetUserNavigationBarItems(),
                    _ => new List<NavigationBarItem>(),
                };

                if (_currentPageKind == NavigationBarPageKind.None)
                    _navigationService.IsNavigationBarShown = false;

                foreach (var item in items)
                    _navigationService.NavigationBarItems.Add(item);

                _navigationService.IsNavigationBarShown = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
