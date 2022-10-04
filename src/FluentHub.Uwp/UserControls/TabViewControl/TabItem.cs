using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.TabViewControl
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