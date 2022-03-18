using FluentHub.UserControls.TabViewControl;
using System;

namespace FluentHub.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public ITabView TabView { get; private set; }

        public void CloseTab(Guid tabId)
        {
            throw new NotImplementedException();
        }

        public void Configure(ITabView tabView!!) => TabView = tabView;

        public void Navigate<T>(object parameter = null)
        {
            throw new NotImplementedException();
        }

        public Guid OpenTab<T>(object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void GoToTab(Guid tabId)
        {
            throw new NotImplementedException();
        }
    }
}
