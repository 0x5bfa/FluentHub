using FluentHub.Backend;
using FluentHub.Helpers;
using FluentHub.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
namespace FluentHub.ViewModels.AppSettings
{
    public class AppearanceViewModel : ObservableObject
    {
        #region constructor        
        public AppearanceViewModel(ILogger logger = null)
        {
            _logger = logger;

            _selectedThemeIndex = (int)Enum.Parse<ElementTheme>(ThemeHelper.RootTheme.ToString());
            _selectedLanguageIndex = App.Settings.DefaultLanguages.IndexOf(App.Settings.DefaultLanguage);
            _showRestartMessage = false;

            Themes = new List<string>()
            {
                "Use system setting",
                "Light theme",
                "Dark theme",
            }.AsReadOnly();

            DefaultLanguages = App
                                .Settings
                                .DefaultLanguages
                                .ToList()
                                .AsReadOnly();
        }
        #endregion

        #region fields        
        private readonly ILogger _logger;
        private int _selectedThemeIndex;
        private int _selectedLanguageIndex;
        private bool _showRestartMessage;
        #endregion

        #region properties        
        public ReadOnlyCollection<DefaultLanguageModel> DefaultLanguages { get; private set; }
        public ReadOnlyCollection<string> Themes { get; set; }
        public int SelectedThemeIndex
        {
            get => _selectedThemeIndex;
            set
            {
                if (SetProperty(ref _selectedThemeIndex, value))
                {
                    ThemeHelper.RootTheme = (ElementTheme)value;
                    _logger?.Info("Theme changed to {0}", ThemeHelper.RootTheme);
                }
            }
        }

        public int SelectedLanguageIndex
        {
            get => _selectedLanguageIndex;
            set
            {
                if (SetProperty(ref _selectedLanguageIndex, value))
                {
                    App.Settings.DefaultLanguage = DefaultLanguages[value];
                    _logger?.Info("Language changed to {0}", App.Settings.DefaultLanguage);

                    ShowRestartMessage = App.Settings.CurrentLanguage.ID != DefaultLanguages[value].ID;
                }
            }
        }

        public bool ShowRestartMessage
        {
            get => _showRestartMessage;
            set => SetProperty(ref _showRestartMessage, value);
        }
        #endregion
    }
}