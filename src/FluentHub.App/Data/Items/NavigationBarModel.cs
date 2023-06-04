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
                    if ((value.PageKind == NavigationBarPageKind.User && Parameter?.UserLogin is not null) ||
                        (value.PageKind == NavigationBarPageKind.Repository && Parameter?.RepositoryName is not null) ||
                        (value.PageKind == NavigationBarPageKind.Organization && Parameter?.UserLogin is not null))
                    {
                        var service = App.Current.Services.GetRequiredService<INavigationService>();
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

        private NavigationBarPageKind _PageKind;
        public NavigationBarPageKind PageKind
        {
            get => _PageKind;
            set
            {
                _PageKind = value;
                OnPropertyChanged(nameof(IsNavigationBarShown));
            }
        }

        public bool IsNavigationBarShown
            => PageKind != NavigationBarPageKind.None;

        public FrameNavigationParameter Parameter { get; set; } = new();
    }
}
