using FluentHub.Uwp.Utils;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace FluentHub.Uwp.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }

        Frame Frame { get; }

        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }
    }
}