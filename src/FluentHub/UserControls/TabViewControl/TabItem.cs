using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.TabViewControl
{
    public class TabItem : ObservableObject
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private muxc.FontIconSource _iconSource;
        public muxc.FontIconSource IconSource
        {
            get => _iconSource;
            set => SetProperty(ref _iconSource, value);
        }

        private bool _useHomeView;
        public bool UseHomeView
        {
            get => _useHomeView;
            set => SetProperty(ref _useHomeView, value);
        }

        public ObservableCollection<string> PageUrls { get; set; } = new();

        public int NavigationIndex => PageUrls.Count() - 1;
    }
}
