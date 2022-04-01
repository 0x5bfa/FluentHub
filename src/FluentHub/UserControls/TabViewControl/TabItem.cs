using FluentHub.Backend;
using FluentHub.Services.Navigation;
using FluentHub.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls.TabViewControl
{
    public class TabItem : ObservableObject, ITabViewItem
    {
        public TabItem()
        {
            _logger = App.Current.Services.GetService<ILogger>();
            Guid = Guid.NewGuid();
            Frame = new();
            NavigationHistory = new();

            Frame.Navigating += OnFrameNavigating;
        }
#if DEBUG
        ~TabItem() => System.Diagnostics.Debug.WriteLine("~TabItem");
#endif

        private readonly ILogger _logger;
        public Guid Guid { get; }
        public Frame Frame { get; }
        public NavigationHistory<PageNavigationEntry> NavigationHistory { get; }

        private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            switch (e.NavigationMode)
            {
                case NavigationMode.New:
                    NavigationHistory.NavigateTo(new PageNavigationEntry(), NavigationHistory.CurrentItemIndex + 1);
                    break;

                case NavigationMode.Back:
                    NavigationHistory.GoBack();
                    break;

                case NavigationMode.Forward:
                    NavigationHistory.GoForward();
                    break;
            }

            _logger?.Info("ITabViewItem.OnFrameNavigating [Page: {0}, Parameter: {1}, NavigationMode: {2}]",
                          e.SourcePageType,
                          e.Parameter,
                          e.NavigationMode);
        }
    }
}