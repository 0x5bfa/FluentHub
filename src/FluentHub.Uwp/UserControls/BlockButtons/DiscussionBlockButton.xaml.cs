using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Views.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.BlockButtons
{
    public sealed partial class DiscussionBlockButton : UserControl
    {
        #region dprops
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                  nameof(Discussion),
                  typeof(DiscussionBlockButtonViewModel),
                  typeof(DiscussionBlockButton),
                  new PropertyMetadata(null)
                );

        public DiscussionBlockButtonViewModel ViewModel
        {
            get => (DiscussionBlockButtonViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public DiscussionBlockButton() => InitializeComponent();

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
