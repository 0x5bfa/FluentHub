using FluentHub.Helpers;
using FluentHub.ViewModels.AppSettingsPages;
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

namespace FluentHub.Views.AppSettingsPages
{
    public sealed partial class GeneralSettingsPage : Page
    {
        public GeneralSettingsPage()
        {
            this.InitializeComponent();
            themeComboBox.PlaceholderText = ThemeHelper.ActualTheme.ToString();
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
                }
            }
        }

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
