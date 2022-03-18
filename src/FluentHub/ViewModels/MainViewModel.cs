using FluentHub.UserControls.TabViewControl;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<TabItem> MainTabItems { get; private set; } = new();

        public int SelectedTabIndex { get; set; }
    }
}