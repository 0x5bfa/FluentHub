// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Data.Items
{
	public class NavigationHistory : ObservableObject
	{
		public NavigationHistory()
		{
			_Items = new();
			Items = new(_Items);

			_CanGoBack = _CanGoForward = false;
			_CurrentItem = default;
			_CurrentItemIndex = -1;
		}

		private bool _CanGoBack;
		public bool CanGoBack
		{
			get => _CanGoBack;
			private set => SetProperty(ref _CanGoBack, value);
		}

		private bool _CanGoForward;
		public bool CanGoForward
		{
			get => _CanGoForward;
			private set => SetProperty(ref _CanGoForward, value);
		}

		private NavigationHistoryItem? _CurrentItem;
		public NavigationHistoryItem? CurrentItem
		{
			get => _CurrentItem;
			private set => SetProperty(ref _CurrentItem, value);
		}

		private readonly ObservableCollection<NavigationHistoryItem> _Items;
		public ReadOnlyObservableCollection<NavigationHistoryItem> Items { get; }

		private int _CurrentItemIndex;
		public int CurrentItemIndex
		{
			get => _CurrentItemIndex;
			set
			{
				if (value == -1)
					CurrentItem = default;
				else if (value >= 0 && value <= _Items.Count)
					CurrentItem = _Items[value];
				else
					throw new ArgumentOutOfRangeException(nameof(value));

				SetProperty(ref _CurrentItemIndex, value);
				Update();
			}
		}

		public NavigationHistoryItem this[int index]
			=> Items[index];

		public bool GoBack()
		{
			if (CanGoBack)
			{
				CurrentItemIndex--;

				// Update NavigationBar
				UpdateNavigationBar(true);

				Update();

				return true;
			}

			return false;
		}

		public bool GoForward()
		{
			if (CanGoForward)
			{
				CurrentItemIndex++;

				// Update NavigationBar
				UpdateNavigationBar(false);

				Update();

				return true;
			}

			return false;
		}

		public void NavigateTo(NavigationHistoryItem item)
		{
			_Items.Add(item);

			CurrentItemIndex = _Items.Count - 1;

			Update();
		}

		public void NavigateTo(NavigationHistoryItem item, int index)
		{
			// Valid
			if (index >= 0 && index <= _Items.Count)
			{
				if (index == 0)
					ClearHistory();

				while (index < _Items.Count)
					_Items.RemoveAt(_Items.Count - 1);

				NavigateTo(item);
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(index));
			}
		}

		public void ClearHistory()
		{
			_Items.Clear();

			CurrentItemIndex = -1;

			Update();
		}

		private void Update()
		{
			CanGoBack = CurrentItemIndex > 0;
			CanGoForward = CurrentItemIndex < _Items.Count - 1;
		}

		public static void SetCurrentItem(string header, string description, string url, IconSource icon)
		{
			INavigationService navigationService;
			navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

			currentItem.Header = header;
			currentItem.Description = description;
			currentItem.Icon = icon;
		}

		private void UpdateNavigationBar(bool isBackNavigation = false)
		{
			var previousItem = isBackNavigation ? Items[CurrentItemIndex + 1] : Items[CurrentItemIndex - 1];

			var _navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentTabNavigationBar = _navigationService.TabView.SelectedItem.NavigationBar;
			if (currentTabNavigationBar is null)
				return;

			if (CurrentItem.PageKind is NavigationPageKind.None)
			{
				currentTabNavigationBar.NavigationBarItems = new();
				currentTabNavigationBar.PageKind = CurrentItem.PageKind;
				currentTabNavigationBar.Parameter = CurrentItem.Context ?? new();

				return;
			}

			// Generate new navigation bar items
			if (previousItem.PageKind != CurrentItem.PageKind)
			{
				currentTabNavigationBar.PageKind = CurrentItem.PageKind;

				if (currentTabNavigationBar.NavigationBarItems.Count != 0)
					currentTabNavigationBar.NavigationBarItems.Clear();

				// Generate items
				var items = CurrentItem.PageKind switch
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

			// Select item
			foreach (var item in currentTabNavigationBar.NavigationBarItems)
			{
				if (item.PageItemKey == CurrentItem.PageKey)
				{
					currentTabNavigationBar.SelectedNavigationBarItem = item;
					break;
				}
			}
		}
	}
}
