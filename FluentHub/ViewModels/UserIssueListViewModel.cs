using FluentHub.DataModels;
using FluentHub.UserControls;
using FluentHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels
{
    public class UserIssueListViewModel
    {
        public static ObservableCollection<Item> Items { get; private set; }

        public UserIssueListViewModel()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
            Items.Add(new Item { Title = "Title" });
        }
    }

    public class Item
    {
        public bool IsOpened { get; set; } = false;
        public string StatusGlyph { get; set; } = "\uE9E6";
        public SolidColorBrush StatusGlyphForeground 
        {
            get
            {
                if (IsOpened)
                {
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x82, 0x56, 0xd0));
                }
                else
                {
                    // #57ab5a
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x57, 0xab, 0x5a));
                }
            }
            private set
            {
            }
        }
        public string RepoFullName { get; set; } ="[repoFullName]";
        public string TimeAgo { get; set; } = "[timeAgo]";
        public string Title { get; set; } = "[title]";
    }

}
