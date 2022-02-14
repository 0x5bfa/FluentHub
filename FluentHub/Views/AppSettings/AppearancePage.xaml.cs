using FluentHub.Helpers;
using FluentHub.ViewModels;
using FluentHub.ViewModels.AppSettings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AppearancePage : Page
    {
        public static SettingsViewModel Settings { get; private set; } = new SettingsViewModel();

        public AppearancePage()
        {
            this.InitializeComponent();

            themeComboBox.PlaceholderText = ThemeHelper.ActualTheme.ToString();
            languageComboBox.PlaceholderText = new CultureInfo(ApplicationLanguages.PrimaryLanguageOverride).NativeName;
        }

         private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             if (themeComboBox.SelectedItem != null)
             {
                 ComboBoxItem cbi = (ComboBoxItem)themeComboBox.SelectedItem;

                 var selectedTheme = cbi.Tag.ToString();

                 if (selectedTheme != null)
                 {
                     ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
                    Settings.AppTheme = selectedTheme;
                 }
             }
         }

       /* private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (themeComboBox.SelectedItem != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)themeComboBox.SelectedItem;

                var selectedTheme = cbi.Tag.ToString();

                if (selectedTheme == "Dark")
                {
                    Settings.AppTheme = ApplicationTheme.Dark;
                }
                else
                {
                    Settings.AppTheme = ApplicationTheme.Light;
                }
            }
        }*/

        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null)
            {
                LanguageCB lcb = (LanguageCB)languageComboBox.SelectedItem;

                if (lcb.PrimaryLangTag == "win-default")
                {
                    var ci = new CultureInfo(Windows.System.UserProfile.GlobalizationPreferences.Languages[0].ToString());
                    lcb.PrimaryLangTag = ci.Name;
                }

                ApplicationLanguages.PrimaryLanguageOverride = lcb.PrimaryLangTag;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.EnumAvailableLanguages();
        }
    }
}
