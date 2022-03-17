using FluentHub.UserControls.TabViewControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItem> MainTabItems { get; private set; } = new();

        public int SelectedTabIndex { get; set; }

        private string currentPageUrl;
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
                    if (MainTabItems.Count() != 0)
                    {
                        int index = MainTabItems[SelectedTabIndex].NavigationIndex;

                        if (index >= 0)
                        {
                            return MainTabItems[SelectedTabIndex].PageUrls[index];
                        }
                        else return null;
                    }

                    return null;
                }
            }
            set
            {
                SetProperty(ref currentPageUrl, value);
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
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
