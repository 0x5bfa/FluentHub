using FluentHub.Services.Navigation;
using FluentHub.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.TabViewControl
{
    public class TabItem : ObservableObject, ITabItemView
    {
        public TabItem()
        {
            PageUrls = new();
            NavigationHistory = new();
        }
        public Guid Guid { get; set; }

        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private muxc.FontIconSource _iconSource;
        public muxc.FontIconSource IconSource
        {
            get => _iconSource;
            set => SetProperty(ref _iconSource, value);
        }

        private bool _useHomeView;
        public bool UseHomeView
        {
            get => _useHomeView;
            set => SetProperty(ref _useHomeView, value);
        }

        public ObservableCollection<string> PageUrls { get; set; }

        public int NavigationIndex => PageUrls.Count() - 1;

        public NavigationHistory<PageStackEntry> NavigationHistory { get; }
    }
}
