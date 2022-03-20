using FluentHub.Utils;
using System;
using System.ComponentModel;

namespace FluentHub.Services.Navigation
{
    public interface ITabItemView : INotifyPropertyChanged
    {
        Guid Guid { get; }        
        NavigationHistory<PageNavigationEntry> NavigationHistory { get; }        
    }
}