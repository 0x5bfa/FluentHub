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
			_navigationService = Ioc.Default.GetRequiredService<INavigationService>();
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_currentPageKind = pageKind;
			_currentPageItemKey = itemKey;

			CheckIfNavigationBarShouldBeChanged();
		}

		public void CheckIfNavigationBarShouldBeChanged()
		{
			_navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem.Context =
				_navigationService.TabView.SelectedItem.NavigationBar.Context;

			var currentTabNavigationBar = _navigationService.TabView.SelectedItem.NavigationBar;
			if (currentTabNavigationBar is null)
				return;

			if (_currentPageKind is NavigationPageKind.None)
			{
				currentTabNavigationBar.PageKind = _currentPageKind;
				currentTabNavigationBar.NavigationBarItems = new();

				return;
			}

			if (currentTabNavigationBar.PageKind != _currentPageKind)
			{
				currentTabNavigationBar.PageKind = _currentPageKind;

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
			if (currentTabNavigationBar.NavigationBarItems is null)
				return;

			var selectedCorrectOne = false;

			foreach (var item in currentTabNavigationBar.NavigationBarItems)
			{
				if (item.PageItemKey == _currentPageItemKey)
				{
					currentTabNavigationBar.SelectedNavigationBarItem = item;
					selectedCorrectOne = true;
					break;
				}
			}

			if (!selectedCorrectOne)
				currentTabNavigationBar.SelectedNavigationBarItem = null;
		}
	}
}
