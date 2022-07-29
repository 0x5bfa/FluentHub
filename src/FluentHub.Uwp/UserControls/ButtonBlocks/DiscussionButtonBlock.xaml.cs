using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.Discussions.DiscussionsPage>(
                new FrameNavigationArgs()
                {
                    Login = ViewModel.Item.Repository.Owner.Login,
                    Name = ViewModel.Item.Repository.Name,
                    Number = ViewModel.Item.Number,
                });
        }
    }
}
