using FluentHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItem> MainTabItems { get; private set; } = new ObservableCollection<TabItem>();

        public int SelectedTabIndex { get; set; }

        private string _currentPageUrl;
        public string CurrentPageUrl
        {
            get
            {
                if (SpecifiedPath != null)
                {
                    return SpecifiedPath;
                }
                else
                {
                    int index = MainTabItems[SelectedTabIndex].NavigationIndex;
                    return MainTabItems[SelectedTabIndex].PageUrlList[index];
                }
            }
            set
            {
                _currentPageUrl = value;
                NotifyPropertyChanged(nameof(CurrentPageUrl));
            }
        }

        public string SpecifiedPath { get; set; }

        public Frame MainFrame { get; private set; } = new Frame();

        public Frame RepoMainFrame { get; private set; } = new Frame();

        public void NavigateMainFrame(string url)
        {
            // MainFrame.Navigate(pageType);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
