﻿// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace FluentHub.App.Views
{
	public abstract class LocatablePage : Page
	{
		protected readonly INavigationService _navigationService;

		private readonly ILogger _logger;

		private readonly NavigationPageKind _currentPageKind;

		private readonly NavigationPageKey _currentPageItemKey;

		protected ITabViewItem SelectedTabViewItem
			=> _navigationService.TabView.SelectedItem;

		protected ICommand _pageLoadCommand;

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

		public void ReloadPage()
		{
			if (_pageLoadCommand is null)
				return;

			var command = _pageLoadCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
