using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Toolkit.Uwp;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentHub.Helpers;
using Windows.UI.Xaml;
using FluentHub.Models;

namespace FluentHub.ViewModels.AppSettings
{
    public class LanguageCB
    {
        public string PrimaryLangTag { get ; set; }
        public string CanonicalLangName { get ;set; }
    }

    public class AppearanceViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DefaultLanguageModel> DefaultLanguages { get; private set; }

        public List<string> Themes { get; set; }

        private int selectedThemeIndex = (int)Enum.Parse(typeof(ElementTheme), ThemeHelper.RootTheme.ToString());
        public int SelectedThemeIndex
        {
            get => selectedThemeIndex;
            set
            {
                if (SetProperty(ref selectedThemeIndex, value))
                {
                    ThemeHelper.RootTheme = (ElementTheme)value;
                    SetProperty(ref selectedThemeIndex, value);
                }
            }
        }

        private int selectedLanguageIndex = App.Settings.DefaultLanguages.IndexOf(App.Settings.DefaultLanguage);
        public int SelectedLanguageIndex
        {
            get => selectedLanguageIndex; 
            set
            {
                if (SetProperty(ref selectedLanguageIndex, value))
                {
                    App.Settings.DefaultLanguage = DefaultLanguages[value];

                    if (App.Settings.CurrentLanguage.ID != DefaultLanguages[value].ID)
                    {
                        LoadRestartInfoBar = true;
                    }
                    else
                    {
                        LoadRestartInfoBar = false;
                    }
                }
            }
        }

        private bool loadRestartInfoBar = false;
        public bool LoadRestartInfoBar
        {
            get => loadRestartInfoBar;
            set => SetProperty(ref loadRestartInfoBar, value);
        }

        public AppearanceViewModel()
        {
            Themes = new List<string>()
            {
                "UseSystemSetting".GetLocalized(),
                "ThemeLight".GetLocalized(),
                "ThemeDark".GetLocalized(),
            };


            DefaultLanguages = App.Settings.DefaultLanguages;
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
