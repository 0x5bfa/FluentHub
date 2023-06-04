// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Data.Enums;
using FluentHub.App.Data.Factories;
using FluentHub.App.Data.Items;

namespace FluentHub.App.Views
{
    public abstract class LocatablePage : Page
    {
        private readonly INavigationService _navigationService;

        private readonly ILogger _logger;

        private readonly NavigationBarPageKind _currentPageKind;

        private readonly NavigationBarItemKey _currentPageItemKey;

        public LocatablePage(NavigationBarPageKind pageKind, NavigationBarItemKey itemKey)
        {
            var provider = App.Current.Services;
            _navigationService = provider.GetRequiredService<INavigationService>();
            _logger = provider.GetRequiredService<ILogger>();
            _currentPageKind = pageKind;
            _currentPageItemKey = itemKey;

            CheckIfNavigationBarShouldBeChanged();
        }

        public void CheckIfNavigationBarShouldBeChanged()
        {
            var currentTabItem = _navigationService.TabView.SelectedItem.NavigationBar;
            if (currentTabItem is null)
                return;

            if (currentTabItem.PageKind != _currentPageKind)
            {
                currentTabItem.PageKind = _currentPageKind;

                // Initialize NavigationBar items
                currentTabItem.NavigationBarItems ??= new();
                currentTabItem.NavigationBarItems.Clear();

                // Generate items
                var items = _currentPageKind switch
                {
                    NavigationBarPageKind.Organization => NavigationBarFactory.GetOrganizationNavigationBarItems(),
                    NavigationBarPageKind.Repository => NavigationBarFactory.GetRepositoryNavigationBarItems(),
                    NavigationBarPageKind.User => NavigationBarFactory.GetUserNavigationBarItems(),
                    _ => new List<NavigationBarItem>(),
                };

                // Add generated items
                foreach (var item in items)
                    currentTabItem.NavigationBarItems.Add(item);
            }

            // Select item
            foreach (var item in currentTabItem.NavigationBarItems)
            {
                if (item.PageItemKey == _currentPageItemKey)
                {
                    currentTabItem.SelectedNavigationBarItem = item;
                    break;
                }
            }
        }
    }
}
