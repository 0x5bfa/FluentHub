using FluentHub.Utils;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }
        Frame Frame { get; }
        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}