using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Services.Navigation
{
    public class PageNavigationEntry : ObservableObject
    {
        public PageNavigationEntry() { }

#if DEBUG
        ~PageNavigationEntry() => System.Diagnostics.Debug.WriteLine("~PageNavigationEntry");
#endif

        #region Fields and Properties
        private string _title;
        public string Header { get => _title; set => SetProperty(ref _title, value); }

        private string _description;
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        private string _url;
        public string Url { get => _url; set => SetProperty(ref _url, value); }

        private string _displayurl;
        public string DisplayUrl { get => _displayurl; set => SetProperty(ref _displayurl, value); }

        private IconSource _icon;
        public IconSource Icon { get => _icon; set => SetProperty(ref _icon, value); }
        #endregion
    }
}