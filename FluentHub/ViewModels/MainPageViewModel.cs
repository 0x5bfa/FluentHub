using FluentHub.DataModels;
using FluentHub.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels
{
    public class MainPageViewModel
    {
        public static ObservableCollection<TabItem> MainTabItems { get; set; } = new ObservableCollection<TabItem>();

        public static int SelectedIndex { get; set; }
    }
}
