using FluentHub.Utils;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Services.Navigation
{
    public interface ITabItemView : INotifyPropertyChanged
    {
        Guid Guid { get; set; }
        string Header { get; set; }
        string Description { get; set; }
        NavigationHistory<PageStackEntry> NavigationHistory { get; }
        Microsoft.UI.Xaml.Controls.IconSource Icon { get; set; }        
    }
}