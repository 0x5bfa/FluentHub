// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using Microsoft.Extensions.DependencyInjection;
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
		public bool CanGoBack { get => _CanGoBack; private set => SetProperty(ref _CanGoBack, value); }

		private bool _CanGoForward;
		public bool CanGoForward { get => _CanGoForward; private set => SetProperty(ref _CanGoForward, value); }

		private PageNavigationEntry? _CurrentItem;
		public PageNavigationEntry? CurrentItem { get => _CurrentItem; private set => SetProperty(ref _CurrentItem, value); }

		private readonly ObservableCollection<PageNavigationEntry> _Items;
		public ReadOnlyObservableCollection<PageNavigationEntry> Items { get; }

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

				_CurrentItemIndex = value;

				OnPropertyChanged(nameof(CurrentItemIndex));
				Update();
			}
		}

		public PageNavigationEntry this[int index]
			=> Items[index];

		public bool GoBack()
		{
			if (CanGoBack)
			{
				CurrentItemIndex--;
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
				Update();
				return true;
			}

			return false;
		}

		public void NavigateTo(PageNavigationEntry item)
		{
			_Items.Add(item);
			CurrentItemIndex = _Items.Count - 1;

			Update();
		}

		public void NavigateTo(PageNavigationEntry item, int index)
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
			navigationService = App.Current.Services.GetRequiredService<INavigationService>();
			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

			currentItem.Header = header;
			currentItem.Description = description;
			currentItem.Url = url;
			currentItem.DisplayUrl = "";
			currentItem.Icon = icon;
		}
	}
}
