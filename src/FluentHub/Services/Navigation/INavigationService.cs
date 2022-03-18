using System;

namespace FluentHub.Services.Navigation
{
    public interface INavigationService
    {
        ITabView TabView { get; }
        void Configure(ITabView tabView);
        void Navigate<T>(object parameter = null);
        Guid OpenTab<T>(object parameter = null);
        void CloseTab(Guid tabId);
        void GoToTab(Guid tabId);
    }
}
