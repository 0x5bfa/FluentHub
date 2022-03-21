using FluentHub.Services.Navigation;
using FluentHub.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace FluentHub.UserControls.TabViewControl
{
    public class TabItem : ObservableObject, ITabItemView
    {
        public TabItem()
        {
            Guid = Guid.NewGuid();
            NavigationHistory = new();
        }
#if DEBUG
        ~TabItem() => System.Diagnostics.Debug.WriteLine("~TabItem");
#endif

        public Guid Guid { get; }
        public NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}