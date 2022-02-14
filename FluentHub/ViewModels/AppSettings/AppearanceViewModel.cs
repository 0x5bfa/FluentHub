using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.AppSettings
{
    public class LanguageCB
    {
        public string PrimaryLangTag { get ; set; }
        public string CanonicalLangName { get ;set; }
    }

    public class AppearanceViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<LanguageCB> items = new ObservableCollection<LanguageCB>();
        public ObservableCollection<LanguageCB> Items
        {
            get => items;
            private set
            {
                SetProperty(ref items, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void EnumAvailableLanguages()
        {
            foreach(var langItem in Windows.Globalization.ApplicationLanguages.ManifestLanguages)
            {
                var country = new CultureInfo(langItem).NativeName;

                LanguageCB langCB = new LanguageCB();

                langCB.CanonicalLangName = country;
                langCB.PrimaryLangTag = langItem;

                Items.Add(langCB);
            }

            LanguageCB defaultLangCB = new LanguageCB();
            defaultLangCB.CanonicalLangName = "Windows Default";
            defaultLangCB.PrimaryLangTag = "win-default";

            Items.Insert(0, defaultLangCB);
        }

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
