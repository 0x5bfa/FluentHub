using FluentHub.Octokit.Models;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories.Discussions;
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

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class DiscussionButtonBlock : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Discussion),
                  typeof(DiscussionButtonBlockViewModel),
                  typeof(DiscussionButtonBlock),
                  new PropertyMetadata(null)
                );

        public DiscussionButtonBlockViewModel ViewModel
        {
            get => (DiscussionButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public DiscussionButtonBlock() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(DiscussionPage), ViewModel.Item.Url);
        }
    }
}
