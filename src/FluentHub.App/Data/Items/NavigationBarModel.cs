// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
	public class NavigationBarModel : ObservableObject
	{
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
				if (value is not null && SetProperty(ref _SelectedNavigationBarItem, value))
				{
					// Parameters validation
					if ((value.PageKind == NavigationPageKind.User && Context?.PrimaryText is not null) ||
						(value.PageKind == NavigationPageKind.Repository && Context?.SecondaryText is not null) ||
						(value.PageKind == NavigationPageKind.Organization && Context?.PrimaryText is not null))
					{
						var service = Ioc.Default.GetRequiredService<INavigationService>();
						service.Navigate(
							value.PageToNavigate,
							new FrameNavigationParameter()
							{
								PrimaryText = Context.PrimaryText,
								SecondaryText = Context.SecondaryText,
							});
					}
				}
			}
		}

		private NavigationPageKind _PageKind;
		public NavigationPageKind PageKind
		{
			get => _PageKind;
			set
			{
				_PageKind = value;
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
