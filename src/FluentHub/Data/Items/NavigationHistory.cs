// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Data.Items
{
	public class NavigationHistory : ObservableObject
	{
		private readonly NavigationBarModel _navigationBar;

		private bool _CanReload;
		public bool CanReload
		{
			get => _CanReload;
			set => SetProperty(ref _CanReload, value);
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
				else if (value >= 0 && value < _Items.Count)
					CurrentItem = _Items[value];
				else
					throw new ArgumentOutOfRangeException(nameof(value));

				SetProperty(ref _CurrentItemIndex, value);
			}
		}

		public NavigationHistoryItem this[int index]
			=> Items[index];

		public NavigationHistory(NavigationBarModel navigationBar)
		{
			_navigationBar = navigationBar;
			_Items = new();
			Items = new(_Items);

			_CanReload = false;

			_CurrentItem = default;
			_CurrentItemIndex = -1;
		}

		public bool GoBack()
		{
			if (CurrentItemIndex <= 0)
				return false;

			CurrentItemIndex--;
			ApplyCurrentItemToNavigationBar();
			return true;
		}

		public bool GoForward()
		{
			if (CurrentItemIndex >= _Items.Count - 1)
				return false;

			CurrentItemIndex++;
			ApplyCurrentItemToNavigationBar();
			return true;
		}

		public void NavigateTo(NavigationHistoryItem item)
		{
			_Items.Add(item);

			CurrentItemIndex = _Items.Count - 1;
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
		}

		internal NavigationHistorySnapshot CaptureSnapshot()
			=> new(_Items.ToArray(), CurrentItemIndex);

		internal void RestoreSnapshot(NavigationHistorySnapshot snapshot)
		{
			_Items.Clear();

			foreach (var item in snapshot.Items)
				_Items.Add(item);

			CurrentItemIndex = snapshot.CurrentItemIndex;
			ApplyCurrentItemToNavigationBar();
		}

		private void ApplyCurrentItemToNavigationBar()
		{
			var currentItem = CurrentItem;
			if (currentItem is null)
			{
				_navigationBar.PageKind = NavigationPageKind.None;
				_navigationBar.NavigationBarItems.Clear();
				_navigationBar.SelectWithoutNavigation(null);
				return;
			}

			if (currentItem.PageKind is NavigationPageKind.None)
			{
				_navigationBar.NavigationBarItems.Clear();
				_navigationBar.PageKind = currentItem.PageKind;
				_navigationBar.Context = currentItem.Context ?? new();
				_navigationBar.SelectWithoutNavigation(null);

				return;
			}

			_navigationBar.Context = currentItem.Context ?? new();

			// Generate new navigation bar items
			if (_navigationBar.PageKind != currentItem.PageKind)
			{
				_navigationBar.PageKind = currentItem.PageKind;

				_navigationBar.NavigationBarItems.Clear();

				// Generate items
				var items = currentItem.PageKind switch
				{
					NavigationPageKind.Organization => NavigationBarFactory.GetOrganizationNavigationBarItems(),
					NavigationPageKind.Repository => NavigationBarFactory.GetRepositoryNavigationBarItems(),
					NavigationPageKind.User => NavigationBarFactory.GetUserNavigationBarItems(),
					_ => new List<NavigationBarItem>(),
				};

				// Add generated items
				foreach (var item in items)
					_navigationBar.NavigationBarItems.Add(item);
			}

			if (currentItem.PageKey is NavigationPageKey.None)
			{
				_navigationBar.SelectWithoutNavigation(null);
			}
			else
			{
				var item = _navigationBar.NavigationBarItems
					.FirstOrDefault(candidate => candidate.PageItemKey == currentItem.PageKey);
				_navigationBar.SelectWithoutNavigation(item);
			}
		}
	}

	internal sealed record NavigationHistorySnapshot(
		IReadOnlyList<NavigationHistoryItem> Items,
		int CurrentItemIndex);
}
