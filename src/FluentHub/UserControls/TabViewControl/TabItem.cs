using FluentHub.Services.Navigation;
using FluentHub.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
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
#if DEBUG
        ~TabItem() => System.Diagnostics.Debug.WriteLine("~TabItem");
#endif

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

        private muxc.IconSource _icon;
        public muxc.IconSource Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }
        public ObservableCollection<string> PageUrls { get; set; }
        public NavigationHistory<PageStackEntry> NavigationHistory { get; }
    }
}
