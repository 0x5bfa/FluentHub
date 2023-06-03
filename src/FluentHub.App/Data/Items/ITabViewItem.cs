// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace FluentHub.App.Data.Items
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }

        Frame Frame { get; }

        NavigationHistory NavigationHistory { get; }

        public ObservableCollection<NavigationBarItem>? NavigationBarItems { get; set; }

        public NavigationBarItem? SelectedNavigationBarItem { get; set; }

        public NavigationBarPageKind PageKind { get; set; }

        public bool IsNavigationBarShown { get; }
    }
}
