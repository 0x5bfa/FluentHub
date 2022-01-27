using FluentHub.Models.Items;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserPages
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProfileBlockItem> _items = new ObservableCollection<ProfileBlockItem>();
        public ObservableCollection<ProfileBlockItem> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        //public void SetProfileElements(User user)
        //{
        //    // Location
        //    if (user.Location != "")
        //    {
        //        var item = new ProfileBlockItem();
        //        item.Glygh = "\uEA03";
        //        var elem = new StackPanel();
        //        var textBlock = new TextBlock();
        //        textBlock.Text = user.Location;
        //        textBlock.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
        //        elem.Children.Add(textBlock);
        //        item.ElementContent = elem;
        //        Items.Add(item);
        //    }

        //    if (user.Blog != "")
        //    {
        //        var item = new ProfileBlockItem();
        //        item.Glygh = "\uE9FB";
        //        var elem = new StackPanel();
        //        var hyperlinkButton = new HyperlinkButton();
        //        hyperlinkButton.Content = user.Blog;
        //        hyperlinkButton.NavigateUri = new UriBuilder(user.Blog).Uri;
        //        hyperlinkButton.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
        //        hyperlinkButton.Padding = new Windows.UI.Xaml.Thickness(2);
        //        elem.Children.Add(hyperlinkButton);
        //        item.ElementContent = elem;
        //        Items.Add(item);
        //    }

        //    // Followers/Following
        //    if (user.Followers >= 0 && user.Following >= 0)
        //    {
        //        var item = new ProfileBlockItem();
        //        item.Glygh = "\uEA36";
        //        var elem = new StackPanel();
        //        elem.Orientation = Orientation.Horizontal;
        //        elem.Spacing = 2;

        //        #region Followers
        //        var followersButton = new Button();
        //        followersButton.Style
        //            = Windows.UI.Xaml.Application.Current.Resources
        //            ["ClearButtonStyle"] as Windows.UI.Xaml.Style;
        //        followersButton.Padding = new Windows.UI.Xaml.Thickness(4);

        //        var followersButtonContent = new StackPanel();
        //        followersButtonContent.Spacing = 6;
        //        followersButton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        //        followersButtonContent.Orientation = Orientation.Horizontal;
        //        var followersTextBlock = new TextBlock();
        //        var followersText = new TextBlock();
        //        followersTextBlock.Text = user.Followers.ToString();
        //        followersText.Text = "followers";
        //        followersText.Foreground
        //            = Windows.UI.Xaml.Application.Current.Resources
        //            ["ApplicationSecondaryForegroundThemeBrush"]
        //            as Windows.UI.Xaml.Media.Brush;
        //        followersButtonContent.Children.Add(followersTextBlock);
        //        followersButtonContent.Children.Add(followersText);

        //        followersButton.Content = followersButtonContent;

        //        elem.Children.Add(followersButton);
        //        #endregion

        //        #region Bullet
        //        var bulletText = new TextBlock();
        //        bulletText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
        //        bulletText.Text = "•";
        //        bulletText.Foreground
        //            = Windows.UI.Xaml.Application.Current.Resources
        //            ["ApplicationSecondaryForegroundThemeBrush"]
        //            as Windows.UI.Xaml.Media.Brush;

        //        elem.Children.Add(bulletText);
        //        #endregion

        //        #region Following
        //        var followingButton = new Button();
        //        followingButton.Style
        //            = Windows.UI.Xaml.Application.Current.Resources
        //            ["ClearButtonStyle"] as Windows.UI.Xaml.Style;
        //        followingButton.Padding = new Windows.UI.Xaml.Thickness(4);

        //        var followingButtonContent = new StackPanel();
        //        followingButtonContent.Spacing = 6;
        //        followingButton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        //        followingButtonContent.Orientation = Orientation.Horizontal;
        //        var followingTextBlock = new TextBlock();
        //        followingTextBlock.Text = user.Following.ToString();
        //        var followingText = new TextBlock();
        //        followingText.Text = "following";
        //        followingText.Foreground
        //            = Windows.UI.Xaml.Application.Current.Resources
        //            ["ApplicationSecondaryForegroundThemeBrush"]
        //            as Windows.UI.Xaml.Media.Brush;
        //        followingButtonContent.Children.Add(followingTextBlock);
        //        followingButtonContent.Children.Add(followingText);

        //        followingButton.Content = followingButtonContent;

        //        elem.Children.Add(followingButton);
        //        #endregion

        //        item.ElementContent = elem;
        //        Items.Add(item);
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
