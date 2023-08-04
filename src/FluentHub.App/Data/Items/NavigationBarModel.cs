// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
	public class NavigationBarModel : ObservableObject
	{
		private ObservableCollection<NavigationBarItem>? _NavigationBarItems;
		public ObservableCollection<NavigationBarItem>? NavigationBarItems
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
					if ((value.PageKind == NavigationPageKind.User && Parameter?.UserLogin is not null) ||
						(value.PageKind == NavigationPageKind.Repository && Parameter?.RepositoryName is not null) ||
						(value.PageKind == NavigationPageKind.Organization && Parameter?.UserLogin is not null))
					{
						var service = Ioc.Default.GetRequiredService<INavigationService>();
						service.Navigate(
							value.PageToNavigate,
							new FrameNavigationParameter()
							{
								UserLogin = Parameter.UserLogin,
								RepositoryName = Parameter.RepositoryName,
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

		public FrameNavigationParameter Parameter { get; set; } = new();
	}
}
