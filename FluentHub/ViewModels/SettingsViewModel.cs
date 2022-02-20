using FluentHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.Xaml;

namespace FluentHub.ViewModels
{
    public class SettingsViewModel : SettingsManager
    {
        public SettingsViewModel()
        {
            var supportedLang = ApplicationLanguages.ManifestLanguages;
            DefaultLanguages = new ObservableCollection<DefaultLanguageModel> { new DefaultLanguageModel(null) };

            foreach (var lang in supportedLang)
            {
                DefaultLanguages.Add(new DefaultLanguageModel(lang));
            }
        }
        public bool SetupCompleted
        {
            get => Get(nameof(SetupCompleted), false);
            set => Set(nameof(SetupCompleted), value);
        }

        public bool SetupProgress
        {
            get => Get(nameof(SetupProgress), false);
            set => Set(nameof(SetupProgress), value);
        }

        public string AccessToken
        {
            get => Get(nameof(AccessToken), "");
            set => Set(nameof(AccessToken), value);
        }

        public string AppTheme
        {
            get => Get(nameof(AppTheme), "Default");
            set => Set(nameof(AppTheme), value);
        }

        public string LastOpenedPageFrame
        {
            get => Get(nameof(LastOpenedPageFrame), nameof(Views.UserHomePage));
            set => Set(nameof(LastOpenedPageFrame), value);
        }

        public string AppVersion
        {
            get => Get(nameof(AppVersion), "Unknown version");
            set => Set(nameof(AppVersion), value);
        }

        // will be replaced by a account list json file
        public string AccountsNamesJoinedSlashes
        {
            get => Get(nameof(AppVersion), $"{App.SignedInUserName}");
            set => Set(nameof(AppVersion), value);
        }

        public ObservableCollection<DefaultLanguageModel> DefaultLanguages { get; private set; }

        public DefaultLanguageModel DefaultLanguage
        {
            get => DefaultLanguages.FirstOrDefault(dl => dl.ID == ApplicationLanguages.PrimaryLanguageOverride) ?? DefaultLanguages.FirstOrDefault();
            set => ApplicationLanguages.PrimaryLanguageOverride = value.ID;
        }

        public DefaultLanguageModel CurrentLanguage { get; set; } = new DefaultLanguageModel(ApplicationLanguages.PrimaryLanguageOverride);
    }
}
