// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace FluentHub.Views
{
	public abstract class LocatablePage : Page
	{
		protected readonly INavigationService _navigationService;

		private readonly ILogger _logger;

		private readonly NavigationPageKind _currentPageKind;

		private readonly NavigationPageKey _currentPageItemKey;

		protected ITabViewItem SelectedTabViewItem
			=> _navigationService.TabView.SelectedItem;

		protected ICommand _pageLoadCommand = default!;

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
			var selectedItem = _navigationService.TabView.SelectedItem;
			var currentItem = selectedItem.NavigationHistory.CurrentItem;
			if (currentItem is null)
				return;

			currentItem.Context = selectedItem.NavigationBar.Context;

			var currentTabNavigationBar = selectedItem.NavigationBar;
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

			currentItem.PageKey = _currentPageItemKey;
			currentItem.PageKind = _currentPageKind;

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
