using FluentHub.Utils;
using System;
using System.ComponentModel;

namespace FluentHub.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }
        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}