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

		private readonly NavigationPageKind _currentPageKind;

		private readonly NavigationPageKey _currentPageItemKey;

		public LocatablePage(NavigationPageKind pageKind, NavigationPageKey itemKey)
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
			var currentTabNavigationBar = _navigationService.TabView.SelectedItem.NavigationBar;
			if (currentTabNavigationBar is null)
				return;

			if (currentTabNavigationBar.PageKind != _currentPageKind)
			{
				currentTabNavigationBar.PageKind = _currentPageKind;

				// Initialize NavigationBar items
				currentTabNavigationBar.NavigationBarItems ??= new();
				currentTabNavigationBar.NavigationBarItems.Clear();

				// Generate items
				var items = _currentPageKind switch
				{
					NavigationPageKind.Organization => NavigationBarFactory.GetOrganizationNavigationBarItems(),
					NavigationPageKind.Repository => NavigationBarFactory.GetRepositoryNavigationBarItems(),
					NavigationPageKind.User => NavigationBarFactory.GetUserNavigationBarItems(),
					_ => new List<NavigationBarItem>(),
				};

				// Add generated items
				foreach (var item in items)
					currentTabNavigationBar.NavigationBarItems.Add(item);
			}

			_navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem.PageKey = _currentPageItemKey;
			_navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem.PageKind = _currentPageKind;

			// Select item
			foreach (var item in currentTabNavigationBar.NavigationBarItems)
			{
				if (item.PageItemKey == _currentPageItemKey)
				{
					currentTabNavigationBar.SelectedNavigationBarItem = item;
					break;
				}
			}
		}
	}
}
