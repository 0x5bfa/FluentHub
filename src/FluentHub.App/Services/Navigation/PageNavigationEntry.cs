// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

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
    }
}
