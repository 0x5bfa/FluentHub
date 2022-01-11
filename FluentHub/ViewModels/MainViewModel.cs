using FluentHub.DataModels;
using FluentHub.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItem> MainTabItems { get; set; } = new ObservableCollection<TabItem>();

        public int SelectedIndex { get; set; }

        private string _fullUrl;
        public string FullUrl
        {
            get => _fullUrl;
            set
            {
                _fullUrl = value;
                NotifyPropertyChanged(nameof(FullUrl));
            }
        }

        public void RequestNavigateMainFrame(string url)
        {

        }

        // Move roughly and perform processing such as queries on the moved page.
        public string UnpersedUrlString { get; set; }

        // If the URL is entered, it will not record tha way to the specified page.
        public bool SpecifiedUrl { get; set; } = false;

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
