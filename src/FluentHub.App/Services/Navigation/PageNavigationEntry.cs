// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Items;
using FluentHub.Core.Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.Services.Navigation
{
    public class PageNavigationEntry : ObservableObject
    {
        public PageNavigationEntry()
        {
        }

        private string? _Header;
        public string? Header { get => _Header; set => SetProperty(ref _Header, value); }

        private string? _Description;
        public string? Description { get => _Description; set => SetProperty(ref _Description, value); }

        private string? _Url;
        public string? Url { get => _Url; set => SetProperty(ref _Url, value); }

        private string? _DisplayUrl;
        public string? DisplayUrl { get => _DisplayUrl; set => SetProperty(ref _DisplayUrl, value); }

        private IconSource? _Icon;
        public IconSource? Icon { get => _Icon; set => SetProperty(ref _Icon, value); }

        private string? _UserLogin;
        public string? UserLogin { get => _UserLogin; set => SetProperty(ref _UserLogin, value); }

        private string? _RepositoryName;
        public string? RepositoryName { get => _RepositoryName; set => SetProperty(ref _RepositoryName, value); }

        private ObservableCollection<NavigationBarItem>? _NavigationBarItems;
        public ObservableCollection<NavigationBarItem>? NavigationBarItems { get => _NavigationBarItems; set => SetProperty(ref _NavigationBarItems, value); }

        private NavigationBarItem? _SelectedNavigationBarItem;
        public NavigationBarItem? SelectedNavigationBarItem
        {
            get => _SelectedNavigationBarItem;
            set
            {
                if (SetProperty(ref _SelectedNavigationBarItem, value) || UserLogin is not null)
                {
                    var service = App.Current.Services.GetRequiredService<INavigationService>();
                    service.Navigate(
                        SelectedNavigationBarItem.PageToNavigate,
                        new Models.FrameNavigationParameter()
                        {
                            Login = UserLogin,
                        });
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
    }
}
