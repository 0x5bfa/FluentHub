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
            var currentItem = _navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            if (currentItem is null || currentItem.PageKind == _currentPageKind)
                return;

            currentItem.PageKind = _currentPageKind;

            // Initialize NavigationBar items
            currentItem.NavigationBarItems ??= new();
            currentItem.NavigationBarItems.Clear();

            // Generate items
            var items = _currentPageKind switch
            {
                NavigationBarPageKind.Organization => NavigationBarFactory.GetUserNavigationBarItems(),
                NavigationBarPageKind.Repository => NavigationBarFactory.GetUserNavigationBarItems(),
                NavigationBarPageKind.User => NavigationBarFactory.GetUserNavigationBarItems(),
                _ => new List<NavigationBarItem>(),
            };

            // Add generated items
            foreach (var item in items)
                currentItem.NavigationBarItems.Add(item);

            // Select item
            var selectableItem = currentItem.NavigationBarItems.Where(x => x.PageItemKey == _currentPageItemKey).FirstOrDefault();
            if (selectableItem != default)
                currentItem.SelectedNavigationBarItem = selectableItem;
        }
    }
}
