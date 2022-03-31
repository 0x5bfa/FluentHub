using FluentHub.Services.Navigation;
using System;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Services
{
    public interface INavigationService
    {
        ITabView TabView { get; }
        bool IsConfigured { get; }        
        void Configure(ITabView tabView);
        void Disconnect();
        void Navigate(Type page, object parameter = null);
        void Navigate<T>(object parameter = null) where T : Page;
        Guid OpenTab(Type page, object parameter = null);
        Guid OpenTab<T>(object parameter = null) where T : Page;
        void CloseTab(Guid tabId);
        void GoToTab(Guid tabId);
        void GoBack();
        void GoForward();
        bool CanGoBack();
        bool CanGoForward();
    }
}