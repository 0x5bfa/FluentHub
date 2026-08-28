// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.Data.Items
{
	public class NavigationBarModel : ObservableObject
	{
		private bool _isNavigationSuppressed;

		private ObservableCollection<NavigationBarItem> _NavigationBarItems = new();
		public ObservableCollection<NavigationBarItem> NavigationBarItems
		{
			get => _NavigationBarItems;
			set => SetProperty(ref _NavigationBarItems, value);
		}

		private NavigationBarItem? _SelectedNavigationBarItem;
		public NavigationBarItem? SelectedNavigationBarItem
		{
			get => _SelectedNavigationBarItem;
			set
			{
				if (!SetProperty(ref _SelectedNavigationBarItem, value) ||
					_isNavigationSuppressed ||
					value is null)
					return;

				// Parameters validation
				if ((value.PageKind == NavigationPageKind.User && Context.PrimaryText is not null) ||
					(value.PageKind == NavigationPageKind.Repository && Context.SecondaryText is not null) ||
					(value.PageKind == NavigationPageKind.Organization && Context.PrimaryText is not null))
				{
					var service = Ioc.Default.GetRequiredService<INavigationService>();
					service.Navigate(
						value.PageToNavigate,
						new FrameNavigationParameter()
						{
							PrimaryText = Context.PrimaryText,
							SecondaryText = Context.SecondaryText,
						},
						new SuppressNavigationTransitionInfo());
				}
			}
		}

		public void SelectWithoutNavigation(NavigationBarItem? item)
		{
			_isNavigationSuppressed = true;

			try
			{
				SelectedNavigationBarItem = item;
			}
			finally
			{
				_isNavigationSuppressed = false;
			}
		}

		private NavigationPageKind _PageKind;
		public NavigationPageKind PageKind
		{
			get => _PageKind;
			set
			{
				SetProperty(ref _PageKind, value);
				OnPropertyChanged(nameof(IsNavigationBarShown));
			}
		}

		public bool IsNavigationBarShown
			=> PageKind != NavigationPageKind.None;

		private FrameNavigationParameter _Context = new();
		public FrameNavigationParameter Context
		{
			get => _Context;
			set => SetProperty(ref _Context, value);
		}
	}
}
