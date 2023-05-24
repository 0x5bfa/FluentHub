// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace FluentHub.App.Services.Navigation
{
    public interface ITabViewItem : INotifyPropertyChanged
    {
        Guid Guid { get; }

        Frame Frame { get; }

        NavigationHistory NavigationHistory { get; }
    }
}
