// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Data.Items
{
    public class TabViewItem : ObservableObject, ITabViewItem
    {
        private readonly ILogger _logger;

        public Guid Guid { get; }

        public Frame Frame { get; }

        public NavigationHistory NavigationHistory { get; }

        private ObservableCollection<NavigationBarItem>? _NavigationBarItems;
        public ObservableCollection<NavigationBarItem>? NavigationBarItems { get => _NavigationBarItems; set => SetProperty(ref _NavigationBarItems, value); }

        private NavigationBarItem? _SelectedNavigationBarItem;
        public NavigationBarItem? SelectedNavigationBarItem
        {
            get => _SelectedNavigationBarItem;
            set
            {
                if (value is not null && SetProperty(ref _SelectedNavigationBarItem, value))
                {
                    // Parameters validation
                    if ((value.PageKind == NavigationBarPageKind.User && NavigationHistory.CurrentItem.UserLogin is not null) ||
                        (value.PageKind == NavigationBarPageKind.Repository && NavigationHistory.CurrentItem.RepositoryName is not null) ||
                        (value.PageKind == NavigationBarPageKind.Organization && NavigationHistory.CurrentItem.UserLogin is not null))
                    {
                        var service = App.Current.Services.GetRequiredService<INavigationService>();
                        service.Navigate(
                            SelectedNavigationBarItem.PageToNavigate,
                            new FrameNavigationParameter()
                            {
                                UserLogin = NavigationHistory.CurrentItem.UserLogin,
                                RepositoryName = NavigationHistory.CurrentItem.RepositoryName,
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

        public TabViewItem()
        {
            // Dependency injection
            _logger = App.Current.Services.GetRequiredService<ILogger>();

            // Initialize
            Guid = Guid.NewGuid();
            Frame = new();
            NavigationHistory = new();

            Frame.Navigating += OnFrameNavigating;
        }

        private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            switch (e.NavigationMode)
            {
                case NavigationMode.New:
                    NavigationHistory.NavigateTo(new PageNavigationEntry(), NavigationHistory.CurrentItemIndex + 1);
                    break;
                case NavigationMode.Back:
                    NavigationHistory.GoBack();
                    break;
                case NavigationMode.Forward:
                    NavigationHistory.GoForward();
                    break;
            }

            _logger?.Info("ITabViewItem.OnFrameNavigating [Page: {0}, Parameter: {1}, NavigationMode: {2}]", e.SourcePageType, e.Parameter, e.NavigationMode);
        }
    }
}
