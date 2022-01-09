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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<TabItem> MainTabItems { get; set; } = new ObservableCollection<TabItem>();

        public static int SelectedIndex { get; set; }

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

        // Move roughly and perform processing such as queries on the moved page.
        public string UnpersedUrlString { get; set; }


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
