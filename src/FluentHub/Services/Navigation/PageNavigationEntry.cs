using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Services.Navigation
{
    public class PageNavigationEntry : ObservableObject
    {
        #region constructor
        public PageNavigationEntry(Type page, object parameter = null, NavigationTransitionInfo transitionInfo = null)
        {
            PageStackEntry = new PageStackEntry(page, parameter, transitionInfo);
        }
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
        public PageStackEntry PageStackEntry { get; }
        #endregion
    }
}