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

        NavigationBarModel NavigationBar { get; }
    }
}
