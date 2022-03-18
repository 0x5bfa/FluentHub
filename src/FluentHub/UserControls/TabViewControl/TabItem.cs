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
        public TabItem()
        {
            PageUrls.CollectionChanged += PageUrls_CollectionChanged;
        }

        private void PageUrls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            App.MainViewModel.CurrentPageUrl = (sender as ObservableCollection<string>)[e.NewStartingIndex];
        }

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

        private bool useHomeView;
        public bool UseHomeView
        {
            get => useHomeView;
            set => SetProperty(ref useHomeView, value);
        }

        private bool useOctions;
        public bool UseOctions
        {
            get => useOctions;
            set
            {
                if (value)
                {
                    var iconSourceWithOctions = new muxc.FontIconSource();
                    iconSourceWithOctions.Glyph = IconSource.Glyph;
                    iconSourceWithOctions.FontFamily = Application.Current.Resources["Octions"] as Windows.UI.Xaml.Media.FontFamily;
                    IconSource = iconSourceWithOctions;
                }
                else
                {
                    var iconSourceWithDefault = new muxc.FontIconSource();
                    iconSourceWithDefault.Glyph = IconSource.Glyph;
                    IconSource = iconSourceWithDefault;
                }

                SetProperty(ref useOctions, value);
            }
        }

        public ObservableCollection<string> PageUrls { get; set; } = new();

        public int NavigationIndex
        {
            get => PageUrls.Count() - 1;
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
