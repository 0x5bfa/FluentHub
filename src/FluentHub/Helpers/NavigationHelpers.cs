using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Helpers
{
    public class NavigationHelpers
    {
        public static void AddPageInfoToTabItem(string header, string description, string url, string glyph)
        {
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].Description = description;
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].Header = header;
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].IconSource = new muxc.FontIconSource() { Glyph = glyph };
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].PageUrls.Add(url);
        }
    }
}
