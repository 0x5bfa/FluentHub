using FluentHub.Uwp.Utils;
using System.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }

        Frame Frame { get; }

        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}