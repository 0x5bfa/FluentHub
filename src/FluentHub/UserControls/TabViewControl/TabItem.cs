using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.TabViewControl
{
    public class TabItem : INotifyPropertyChanged
    {
        private string header;
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private muxc.FontIconSource iconSource;
        public muxc.FontIconSource IconSource
        {
            get => iconSource;
            set => SetProperty(ref iconSource, value);
        }

        public ObservableCollection<string> PageUrls { get; set; } = new();

        private int naviationIndex;
        public int NavigationIndex
        {
            get => PageUrls.Count() - 1;
            set => SetProperty(ref naviationIndex, value);
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
