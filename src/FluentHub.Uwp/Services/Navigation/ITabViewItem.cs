using FluentHub.Uwp.Utils;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }
        Frame Frame { get; }
        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}