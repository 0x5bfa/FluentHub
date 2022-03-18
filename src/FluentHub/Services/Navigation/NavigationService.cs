using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FluentHub.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;
        public ITabView TabView { get; private set; }
        public Type NewTabPage { get; set; }

        public void Configure(ITabView tabView!!, Frame frame!!, Type newTabPage!!)
        {
            UnsubscribeEvents();
            TabView = tabView;
            _frame = frame;
            NewTabPage = newTabPage;
            SubscribeEvents();
        }

        private void OnTabViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.FirstOrDefault() is ITabItemView item)
            {
                _frame.Navigate(item.CurrentPage ?? NewTabPage, item.Parameter, new SlideNavigationTransitionInfo
                {
                    Effect = SlideNavigationTransitionEffect.FromRight
                });
            }
        }

        public void Navigate(Type page, object parameter)
        {
            if (TabView.SelectedItem is null)
            {
                TabView.OpenTab(page, parameter, true);
            }
            else
            {
                _frame.Navigate(page, parameter);
            }
        }

        public void Navigate<T>(object parameter = null) where T : Page => Navigate(typeof(T), parameter);

        public Guid OpenTab(Type page, object parameter)
        {
            var item = TabView.OpenTab(page, parameter, true);
            item.CurrentPage = page;
            return item.Guid;
        }
        public Guid OpenTab<T>(object parameter = null) where T : Page => OpenTab(typeof(T), parameter);

        public void GoToTab(Guid tabId)
        {
            var item = TabView.Items.FirstOrDefault(x => x.Guid == tabId);
            if (item != null)
            {
                TabView.SelectedItem = item;
                _frame.Navigate(item.CurrentPage);
            }
        }
        public void CloseTab(Guid tabId) => throw new NotImplementedException();

        private void SubscribeEvents()
        {
            if (TabView != null)
            {
                TabView.SelectionChanged += OnTabViewSelectionChanged;
            }
        }

        private void UnsubscribeEvents()
        {
            if (TabView != null)
            {
                TabView.SelectionChanged -= OnTabViewSelectionChanged;
            }
        }
    }
}