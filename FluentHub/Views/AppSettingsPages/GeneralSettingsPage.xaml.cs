﻿using FluentHub.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views.AppSettingsPages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
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
    }
}
