using FluentHub.UserControls.TabViewControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels
{
    public class MainPageViewModel
    {
        public static void AddNewTabByPath(string path)
        {
            TabItem tabItem = new TabItem()
            {
                Header = null,
                IconSource = null,
            };

            App.MainViewModel.SpecifiedPath = path;
            App.MainViewModel.MainTabItems.Add(tabItem);
            App.MainViewModel.NavigateMainFrame(path);
        }
    }
}
