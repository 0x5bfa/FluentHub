using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Services.Navigation
{
    public class PageNavigationEntry : ObservableObject
    {
        #region constructor
        public PageNavigationEntry() { }

#if DEBUG
        ~PageNavigationEntry() => System.Diagnostics.Debug.WriteLine("~PageNavigationEntry");
#endif
        #endregion

        #region fields
        private string _title;
        private string _description;
        private string _url;
        private IconSource _icon;
        #endregion

        #region properties
        public string Header { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string Url { get => _url; set => SetProperty(ref _url, value); }
        public IconSource Icon { get => _icon; set => SetProperty(ref _icon, value); }
        #endregion
    }
}